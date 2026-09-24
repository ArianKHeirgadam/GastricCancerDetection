NONCODING_REGIONS = {
    "promoter",
    "5UTR",
    "3UTR",
    "enhancer",
    "regulatory_intronic",
    "splice_regulatory",
    "lncRNA",
    "miRNA",
}


def _first_transcript(annotation):
    transcripts = annotation.get("transcript_consequences") or []
    if not transcripts:
        return None

    selected = transcripts[0]
    if not isinstance(selected, dict):
        return None

    return selected


def _first_gene(annotation):
    transcript = _first_transcript(annotation)
    if transcript:
        return (
            transcript.get("gene_symbol")
            or transcript.get("gene_id")
        )
    return annotation.get("gene_symbol") or annotation.get("gene_id")


def _derive_region(annotation):
    transcript = _first_transcript(annotation)
    consequences = []

    if transcript:
        raw = transcript.get("consequence_terms") or []
        if isinstance(raw, str):
            consequences = [raw]
        else:
            consequences = list(raw)

    regulatory = annotation.get("regulatory_feature_consequences") or []
    regulatory_consequences = []
    for item in regulatory:
        if not isinstance(item, dict):
            continue
        raw = item.get("consequence_terms") or []
        if isinstance(raw, str):
            regulatory_consequences.append(raw)
        else:
            regulatory_consequences.extend(raw)

    all_terms = set(consequences + regulatory_consequences)

    if "5_prime_UTR_variant" in all_terms:
        return "5UTR"
    if "3_prime_UTR_variant" in all_terms:
        return "3UTR"
    if "miRNA" in all_terms or "miRNA_target_site" in all_terms:
        return "miRNA"
    if "splice_region_variant" in all_terms:
        return "splice_regulatory"
    if "regulatory_region_variant" in all_terms:
        # VEP identifies a regulatory feature, but a generic
        # regulatory_region_variant does not by itself prove
        # promoter/enhancer class. Keep it explicit and conservative.
        return "regulatory_region"

    return "unknown"


def classify_variant(v, annotation=None):
    annotation = annotation or {}

    transcript = _first_transcript(annotation)
    region = _derive_region(annotation)

    regulatory_features = annotation.get(
        "regulatory_feature_consequences"
    ) or []

    regulatory_annotation = None
    if regulatory_features:
        regulatory_annotation = [
            {
                "id": item.get("regulatory_feature_id"),
                "type": item.get("feature_type"),
                "consequences": item.get("consequence_terms"),
            }
            for item in regulatory_features
            if isinstance(item, dict)
        ]

    consequence_terms = []
    if transcript:
        raw_terms = transcript.get("consequence_terms") or []
        consequence_terms = (
            [raw_terms] if isinstance(raw_terms, str) else list(raw_terms)
        )

    is_noncoding = region in NONCODING_REGIONS

    return {
        "chromosome": v.chrom,
        "position": v.pos,
        "reference": v.ref,
        "alternate": v.alt,
        "gene": _first_gene(annotation),
        "transcript": transcript.get("transcript_id") if transcript else None,
        "region": region,
        "functional_class": consequence_terms[0] if consequence_terms else None,
        "consequence_terms": consequence_terms,
        "vaf": v.vaf,
        "regulatory_annotation": regulatory_annotation,
        "population_frequency": annotation.get("population_frequency"),
        "clinvar_evidence": annotation.get("clinvar_evidence"),
        "dbsnp_identifier": annotation.get("dbsnp_identifier"),
        "annotation_source": annotation.get(
            "annotation_source", "Ensembl VEP REST"
        ),
        "annotation_version": annotation.get("annotation_version"),
        "annotation_status": annotation.get(
            "annotation_status", "not_annotated"
        ),
        "annotation_error": annotation.get("annotation_error"),
        "is_germline": annotation.get("is_germline"),
        "is_noncoding": is_noncoding,
    }
