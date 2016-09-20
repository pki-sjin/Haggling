using Haggling.Model;

namespace Haggling.Factory
{
    public class AutomationFactory
    {
        public static AbstractAutomation Create(Factor factor)
        {
            if (factor.Mode == ModeType.CLIENT)
            {
                switch (factor.Type)
                {
                    case AgentType.CCECSH:
                        {
                            return new CCECSHClientAutomation();
                        }
                    default:
                        {
                            return null;
                        }
                }

            }
            else if (factor.Mode == ModeType.BROWSER)
            {
                switch (factor.Type)
                {
                    case AgentType.CCECSH:
                        {
                            return new CCECSHBrowserAutomation();
                        }
                    default:
                        {
                            return null;
                        }
                }
            }
            else
            {
                return null;
            }
        }
    }
}