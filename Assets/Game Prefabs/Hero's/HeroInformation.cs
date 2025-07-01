using UnityEngine;
using UnityEngine.UI;

public class HeroInformation : MonoBehaviour
{   
    /// <summary>
    /// The maximum health value for the hero.
    /// </summary>
    [Header("Hero Stats")]
    [Tooltip("The maximum health value for the hero.")]
    public int MaxHealth = 100;

    /// <summary>
    /// The current health value for the hero.
    /// </summary>
    [Tooltip("The current health value for the hero.")]
    public int CurrentHealth = 100;

    /// <summary>
    /// The main image representing the hero (e.g., portrait).
    /// </summary>
    [Header("Hero Visuals")]
    [Tooltip("The main image representing the hero (e.g., portrait).")]
    public Sprite HeroImage;

    /// <summary>
    /// The symbol representing the hero (for UI display).
    /// </summary>
    [Tooltip("The symbol representing the hero (for UI display).")]
    public Sprite SymbolSprite;

    public enum HeroClassType { F, M, U, FM, FU, MU, FMU }

    [Header("Hero Class")]
    [Tooltip("The class type of the hero.")]
    public HeroClassType heroClassType;

    [Header("Class Symbols")]
    [Tooltip("Symbol for F class")] public Sprite FSymbol;
    [Tooltip("Symbol for M class")] public Sprite MSymbol;
    [Tooltip("Symbol for U class")] public Sprite USymbol;
    [Tooltip("Symbol for FM class")] public Sprite FMSymbol;
    [Tooltip("Symbol for FU class")] public Sprite FUSymbol;
    [Tooltip("Symbol for MU class")] public Sprite MUSymbol;
    [Tooltip("Symbol for FMU class")] public Sprite FMUSymbol;

    // Reference to the UI Image component to display the symbol
    [Header("Symbol UI Reference")]
    [Tooltip("Assign the UI Image component that will display the hero's class symbol.")]
    public Image symbolImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetSymbolByClass();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetSymbolByClass()
    {
        Sprite classSymbol = null;
        switch (heroClassType)
        {
            case HeroClassType.F: classSymbol = FSymbol; break;
            case HeroClassType.M: classSymbol = MSymbol; break;
            case HeroClassType.U: classSymbol = USymbol; break;
            case HeroClassType.FM: classSymbol = FMSymbol; break;
            case HeroClassType.FU: classSymbol = FUSymbol; break;
            case HeroClassType.MU: classSymbol = MUSymbol; break;
            case HeroClassType.FMU: classSymbol = FMUSymbol; break;
        }
        SetSymbol(classSymbol);
    }

#if UNITY_EDITOR
    // This method is called when a value is changed in the Inspector
    void OnValidate()
    {
        if (Application.isPlaying)
            return;
        SetSymbolByClass();
    }
#endif

    /// <summary>
    /// Sets a new symbol sprite and updates the UI.
    /// </summary>
    /// <param name="newSymbol">The new symbol sprite to set.</param>
    public void SetSymbol(Sprite newSymbol)
    {
        SymbolSprite = newSymbol;
        if (symbolImage != null)
        {
            symbolImage.sprite = newSymbol;
        }
    }
}
