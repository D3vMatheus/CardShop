using System.ComponentModel;

namespace CardShop.Enums
{
    public enum Rarity
    {
        [Description("Common")]
        C,

        [Description("Uncommon")]
        U,
        
        [Description("Rare")]
        R,

        [Description("Super-Rare")]
        SR,

        [Description("Secret-Rare")]
        SEC,

        [Description("Promotional")]
        P,

    }
}
