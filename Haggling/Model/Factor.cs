
namespace Haggling.Model
{
    public class Factor
    {
        public AgentType Type { set; get; }
        public ModeType Mode { set; get; }
    }

    public enum AgentType
    {
        UNDEFINED,
        CCECSH, // 上文众申
        ZMHH    // 郑棉黄海
    }

    public enum ModeType
    {
        UNDEFINED,
        BROWSER,
        CLIENT
    }
}