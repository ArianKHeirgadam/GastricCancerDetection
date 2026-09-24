import sys
from pathlib import Path
sys.path.insert(0, str(Path(__file__).resolve().parents[2] / 'PythonScripts'))
from cfdna_score_engine import score, detectability
from cfdna_simulator import simulate
from noncoding_hotspot import classify_variant
from vcf_parser import Variant


def test_scores_are_bounded():
    assert 0 <= score({'biological':1,'statistical':1,'whitespace':1,'validation_gap':1,'early_stage':1,'specificity':1,'cfdna':1}) <= 1
    assert 0 <= detectability({'signal_strength':1,'dilution_resistance':1,'low_depth_feasibility':1,'regional_redundancy':1,'early_stage':1}) <= 1


def test_dilution_labels_are_in_silico():
    rows = simulate(.4, .01, informative_sites=20)
    assert len(rows) == 7
    assert all(r['label'] == 'in-silico estimate' for r in rows)


def test_non_coding_contract_does_not_invent_annotation():
    v = Variant('1', 100, 'A', 'G', .1, {})
    row = classify_variant(v)
    assert row['region'] == 'unknown'
    assert row['gene'] is None


if __name__ == '__main__':
    test_scores_are_bounded(); test_dilution_labels_are_in_silico(); test_non_coding_contract_does_not_invent_annotation(); print('Python Phase 1 smoke tests: PASS')
