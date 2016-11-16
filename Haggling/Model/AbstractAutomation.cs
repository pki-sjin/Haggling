using System;

namespace Haggling.Model
{
    public abstract class AbstractAutomation
    {
        public abstract bool validate();

        public abstract bool execute(Script script);

        public abstract void clean();

        public abstract void dispose();

        public abstract Array getResponseTime();

        public abstract void testScript(Script script);

        public abstract int prepare(Script script, DateTime targetTime);
    }
}