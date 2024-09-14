using System.ComponentModel;

namespace CardShop.Enums
{
    public enum Quality
    {
        [Description("Normal")]
        Normal,

        [Description("Alternative Art")]
        Alt_Art,

        [Description("Foil")]
        Foil,

        [Description("Textured")]
        Textured,

        [Description("Pre Realease")]
        Pre_Rel,

        [Description("Box Topper")]
        Box_Topper,

        [Description("Full Art")]
        Full_Art,

        [Description("Stamp")]
        Stamp,
    }
}
