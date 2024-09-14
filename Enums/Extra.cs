using System.ComponentModel;

namespace CardShop.Enums
{
    public enum Extra
    {
        [Description("Mint")]
        M,

        [Description("Nearly Mint")]
        NM,

        [Description("Slightly Played")]
        SP,

        [Description("Moderately Played")]
        MP,

        [Description("Heavly Played")]
        HP,

        [Description("Damaged")]
        D
    }
}
