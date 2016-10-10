
using System;
using System.Diagnostics;
using System.Windows.Forms;
namespace Haggling.Model
{
    class CCECSHClientAutomation : AbstractAutomation
    {
        public CCECSHClientAutomation()
            : base()
        {
        }

        public override bool execute(Script script)
        {
            return false;
        }

        public override void clean()
        {
        }

        public override void dispose()
        {

        }

        public override bool validate()
        {
            return false;
        }

        public override long getResponseTime()
        {
            return 0;
        }
    }
}
