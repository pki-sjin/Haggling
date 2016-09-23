
namespace Haggling.Model
{
    public abstract class AbstractAutomation
    {
        public abstract bool validate();

        public abstract bool execute(Script script);

        public abstract void clean();

        public abstract void dispose();
    }
}