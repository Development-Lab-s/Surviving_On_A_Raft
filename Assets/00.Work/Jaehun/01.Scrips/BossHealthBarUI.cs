using _00.Work.CheolYee._01.Codes.Agents;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarUI : MonoBehaviour
{
    [SerializeField] private Image fill;

    private AgentHealth _hp;

    public void Bind(AgentHealth hp)
    {
        // 기존 구독 해제
        if (_hp != null)
        {
            _hp.onHit.RemoveListener(UpdateBar);
            _hp.onDeath.RemoveListener(UpdateBar);
        }

        _hp = hp;

        if (_hp != null)
        {
            _hp.onHit.AddListener(UpdateBar);
            _hp.onDeath.AddListener(UpdateBar);
            gameObject.SetActive(true);
            UpdateBar();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (_hp != null)
        {
            _hp.onHit.RemoveListener(UpdateBar);
            _hp.onDeath.RemoveListener(UpdateBar);
        }
    }
    public void Refresh() => UpdateBar();

    private void UpdateBar()
    {
        if (_hp == null || fill == null) return;
        fill.fillAmount = _hp.NormalizedHealth;

    }
}
