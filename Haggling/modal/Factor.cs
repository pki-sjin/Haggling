using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Haggling.Modal
{
    public class Factor
    {
        private AgentType type;
        private ModeType mode;
    }

    public enum AgentType
    {
        CCECSH // 上文众申
    }

    public enum ModeType
    {
        BROWSER,
        CLIENT
    }
}