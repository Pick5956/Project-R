using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {  get; private set; }
    public PlayerController PlayerController;
    public float m_PlayerMaxHp = 200f;
    public float HpSmoothSpeed;
    private float m_CurrentHp;
    private float m_CurrentHpPercent;
    private float m_TargetHpPercent;
    public UIDocument UIDoc;
    private VisualElement m_HpPanel;
    private VisualElement m_HpGauge;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void ChangePlayerHp(float hp)
    {
        m_CurrentHp += hp;
        m_CurrentHp = Mathf.Clamp(m_CurrentHp, 0, m_PlayerMaxHp);

        m_TargetHpPercent = (m_CurrentHp / m_PlayerMaxHp)*100;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_HpPanel = UIDoc.rootVisualElement.Q<VisualElement>("HpPanel");
        m_HpGauge = m_HpPanel.Q<VisualElement>("HpGauge");
        ChangePlayerHp(m_PlayerMaxHp);
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentHpPercent = Mathf.Lerp(m_CurrentHpPercent, m_TargetHpPercent, Time.deltaTime * HpSmoothSpeed);
        m_HpGauge.style.width = Length.Percent(m_CurrentHpPercent);
    }
}
