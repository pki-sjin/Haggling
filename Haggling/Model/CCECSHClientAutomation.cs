
using System;
using System.Diagnostics;
using System.Windows.Forms;
using TestStack.White.Factory;
using TestStack.White.UIItems.Finders;
namespace Haggling.Model
{
    class CCECSHClientAutomation : AbstractAutomation
    {
        private string processName = "BIJIE";
        private TestStack.White.UIItems.WindowItems.Window window;
        private TestStack.White.UIItems.WindowItems.Window code;
        private TestStack.White.Application application;
        private TestStack.White.UIItems.WindowItems.Window price;
        private TestStack.White.UIItems.WindowItems.Window count;
        private TestStack.White.UIItems.WindowItems.Window reset;
        private TestStack.White.UIItems.WindowItems.Window submit;


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
            if (processName != null)
            {
                var processes = Process.GetProcessesByName(processName);
                if (processes.Length > 0)
                {
                    try
                    {
                        application = TestStack.White.Application.Attach(processName);
                        window = application.GetWindow(SearchCriteria.ByAutomationId("FormMain"), InitializeOption.WithCache);
                        var panelBottom = window.ModalWindow(SearchCriteria.ByAutomationId("panelBottom"), InitializeOption.WithCache);
                        var frmDealMain = panelBottom.ModalWindow(SearchCriteria.ByAutomationId("FrmDealMain"), InitializeOption.WithCache);
                        var panelDeal = frmDealMain.ModalWindow(SearchCriteria.ByAutomationId("panelDeal"), InitializeOption.WithCache);
                        var frmDealBuy = panelDeal.ModalWindow(SearchCriteria.ByAutomationId("FrmDealBuy"), InitializeOption.WithCache);
                        var  panelDeal2 = frmDealBuy.ModalWindow(SearchCriteria.ByAutomationId("panelDeal"), InitializeOption.WithCache);

                        code = panelDeal2.ModalWindow(SearchCriteria.ByAutomationId("txtCode"), InitializeOption.WithCache);
                        price = panelDeal2.ModalWindow(SearchCriteria.ByAutomationId("txtPrice"), InitializeOption.WithCache);
                        count = panelDeal2.ModalWindow(SearchCriteria.ByAutomationId("txtCount"), InitializeOption.WithCache);


                        reset = panelDeal2.ModalWindow(SearchCriteria.ByAutomationId("btn_setting"), InitializeOption.WithCache);
                        submit = panelDeal2.ModalWindow(SearchCriteria.ByAutomationId("btn_sure"), InitializeOption.WithCache);

                        return true;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }

            return false;
        }
    }
}
