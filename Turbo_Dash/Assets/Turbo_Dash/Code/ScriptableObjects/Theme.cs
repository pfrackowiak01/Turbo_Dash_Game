using UnityEngine;

[CreateAssetMenu(fileName = "ThemeData", menuName = "ScriptableObject/Theme")]
public class Theme : ScriptableObject
{
    [Header("Theme Info")]
    [SerializeField] private int index;
    [SerializeField] private string themeName;
    [SerializeField] private bool unlocked;

    [Header("Theme Textures")]
    [SerializeField] private Material tubeInside;
    [SerializeField] private Material tubeOutside;
    [SerializeField] private Material wall;
    [SerializeField] private Material obstacle;
    [SerializeField] private Material space;
    [SerializeField] private Material player;
    [SerializeField] private Material trail;

    // Hermetyzacja w postaci Enkapsulacji
    public int Index { get => index; set => index = value; }
    public string ThemeName { get => themeName; set => themeName = value; }
    public bool Unlocked { get => unlocked; set => unlocked = value; }
    public Material TubeInside { get => tubeInside; set => tubeInside = value; }
    public Material TubeOutside { get => tubeOutside; set => tubeOutside = value; }
    public Material Wall { get => wall; set => wall = value; }
    public Material Obstacle { get => obstacle; set => obstacle = value; }
    public Material Space { get => space; set => space = value; }
    public Material Player { get => player; set => player = value; }
    public Material Trail { get => trail; set => trail = value; }

}

