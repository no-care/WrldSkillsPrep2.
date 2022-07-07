using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Poprijonok
{
    
    public partial class AgentPage : Page
    {
        public AgentPage()
        {
            InitializeComponent();
            FirstInit();
        }
        private void FirstInit()
        {
            var FilterParms = PoprijonokEntities.GetContext().Agents_type.ToList();
            FilterParms.Insert(0, new Agents_type { title = "Все типы" });

            var agents = PoprijonokEntities.GetContext().Agents.ToList();

            ShowAgentData(agents);
            FilterFiled.ItemsSource = FilterParms;
        }

        
        private void ShowAgentData(List<Agents> agents, int PageSize = 10, int ToSkip = 0)
        {
            List<Agnt> AgentsList = new List<Agnt>();


            foreach (var agent in agents)
            {
                var sales = PoprijonokEntities.GetContext().Agent_release_history.Where(p => p.agent_ID == agent.agent_ID).ToList();
                int SalesCount = 0;

                if (sales.Any())
                {
                    foreach (var sale in sales)
                    {
                        SalesCount += sale.qty;
                    }
                }

                int discount = CalculateDiscount(SalesCount);

                AgentsList.Add(new Agnt()
                {
                    ID = agent.agent_ID,
                    Discount = discount,
                    Image = agent.newLogo,
                    Phone = agent.phone,
                    Priority = agent.priority,
                    Sales = SalesCount,
                    Title = agent.Agents_type.title + " | " + agent.title,
                    AgentColor = discount == 25 ? "LightGreen" : "Transparent"
                });
            }

            LViewAgents.ItemsSource = AgentsList.Skip(ToSkip).Take(PageSize);
        }

      
        private int CalculateDiscount(int SalesCount)
        {
            if (SalesCount >= 10000 && SalesCount < 50000)
            {
                return 5;
            }
            else if (SalesCount >= 50000 && SalesCount < 150000)
            {
                return 10;
            }
            else if (SalesCount >= 150000 && SalesCount < 500000)
            {
                return 20;
            }
            else if (SalesCount >= 500000)
            {
                return 25;
            }
            else
            {
                return 0;
            }
        }
        private void FilterData()
        {
            var agents = PoprijonokEntities.GetContext().Agents.ToList();

            if (FilterFiled.SelectedIndex > 0)
            {
                var type_id = (FilterFiled.SelectedItem as Agents_type).type_ID;

                agents = PoprijonokEntities.GetContext().Agents.Where(p => p.Agents_type.type_ID == type_id).ToList();
            }

            var TextToSearch = SearchField.Text.ToLower();

            agents = agents.Where(p => p.title.ToLower().Contains(TextToSearch) || p.phone.ToLower().Contains(TextToSearch) || p.email.ToLower().Contains(TextToSearch)).ToList();

            if (agents.Count == 0)
            {
                MessageBox.Show("Данные по указанным фильтрам не найдены");
                SearchField.Text = "";

                var agentNew = PoprijonokEntities.GetContext().Agents.ToList();

                ShowAgentData(agentNew);
            }

            ShowAgentData(agents);

        }
        private void FilterFiled_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterData();
        }

        private void AddAgent_Click(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new AddAgentPage());
        }

        private void SearchField_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

 
        private void DeleteAgent_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = LViewAgents.SelectedItems[0];

            if (selectedItems != null)
            {
                var agentToDelete = selectedItems as Agnt;

                var agent = PoprijonokEntities.GetContext().Agents.Where(p => p.agent_ID == agentToDelete.ID).FirstOrDefault();
                var agentAddress = PoprijonokEntities.GetContext().Agent_address.Where(p => p.Agent_ID == agentToDelete.ID).FirstOrDefault();

                if (agent.Agent_release_history.Count != 0)
                {
                    MessageBox.Show("У данного агента имеется история реализации продукта, его удаление запрещено!");
                    return;
                }

                if (agentAddress != null)
                    PoprijonokEntities.GetContext().Agent_address.Remove(agentAddress);

                if (agent != null)
                    PoprijonokEntities.GetContext().Agents.Remove(agent);

                try
                {
                    PoprijonokEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    FirstInit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("Для удаления сначала необходимо выбрать агента!");
                return;
            }
        }
    }
}
