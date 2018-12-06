using DataLayer;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using TestShop;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ShopLogic shoplogic = new ShopLogic();
        string deleteThis;
        string doWhat;
        ClientViewer client;

        public MainWindow()
        {
            DataGeneration datagen = new DataGeneration();
            shoplogic.ParseData(datagen.GiveData());
            InitializeComponent();
            updateLists();
        }

        private void showProductsButton_Click(object sender, RoutedEventArgs e)
        {
            clientView.Visibility = Visibility.Collapsed;
            eventView.Visibility = Visibility.Collapsed;
            List<ProductViewer> items = new List<ProductViewer>();
            for (int i = 0; i < shoplogic.shop.Stock.Count(); i++)
            {
                items.Add(new ProductViewer() { Name = shoplogic.shop.Stock[i].Name, Quantity = 1, Price = shoplogic.shop.Stock[i].Price });
            }
            productView.ItemsSource = items;
            viewType.Content = "Products";
            productView.Visibility = Visibility.Visible;
        }

        private void showClientsButton_Click(object sender, RoutedEventArgs e)
        {
            productView.Visibility = Visibility.Collapsed;
            eventView.Visibility = Visibility.Collapsed;
            List<ClientViewer> items = new List<ClientViewer>();
            for (int i = 0; i < shoplogic.shop.Clients.Count(); i++)
            {
                items.Add(new ClientViewer() { Name = shoplogic.shop.Clients[i].Name, Balance = shoplogic.shop.Clients[i].Money, BasketValue = shoplogic.ValueOfBasket(shoplogic.shop.Clients[i]) });
            }
            clientView.ItemsSource = items;
            viewType.Content = "Clients";
            clientView.Visibility = Visibility.Visible;
        }

        private void showEventsButton_Click(object sender, RoutedEventArgs e)
        {
            productView.Visibility = Visibility.Collapsed;
            clientView.Visibility = Visibility.Collapsed;
            List<EventViewer> items = new List<EventViewer>();
            for (int i = 0; i < shoplogic.shop.Events.Count(); i++)
            {
                items.Add(new EventViewer() { Name = shoplogic.shop.Events[i].Name, Value = "nothing" });
            }
            eventView.ItemsSource = items;
            viewType.Content = "Events";
            eventView.Visibility = Visibility.Visible;
        }

        private void checkoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (clientView.Visibility == Visibility.Collapsed)
            {
                productView.Visibility = Visibility.Collapsed;
                eventView.Visibility = Visibility.Collapsed;
                clientView.Visibility = Visibility.Visible;
                showWarningLabel("You need to be in Client view!");
            }
            else
            {
                ClientViewer clientToDelete = (ClientViewer)clientView.SelectedItem;
                for (int i = 0; i < shoplogic.shop.Clients.Count(); i++)
                {
                    if(clientToDelete.Name == shoplogic.shop.Clients[i].Name)
                    {
                        if (shoplogic.Checkout(shoplogic.shop.Clients[i]))
                        {
                            updateLists();
                        }
                        else
                        {
                            showWarningLabel("You can't checkout " + clientToDelete.Name);
                        }
                    }
                }
            }
        }

        private void addClientButton_Click(object sender, RoutedEventArgs e)
        {
            mainPanel.Visibility = Visibility.Collapsed;
            addPanel.Visibility = Visibility.Visible;
            deleteThis = "Client";
            message.Content = "Add Client";
            moneyLabel.Content = "Money";

        }

        private void addProductButton_Click(object sender, RoutedEventArgs e)
        {
            mainPanel.Visibility = Visibility.Collapsed;
            addPanel.Visibility = Visibility.Visible;
            deleteThis = "Product";
            message.Content = "Add Product";
            moneyLabel.Content = "Price";
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            addPanel.Visibility = Visibility.Collapsed;
            mainPanel.Visibility = Visibility.Visible;
            updateLists();
            success.Visibility = Visibility.Collapsed;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(nameBox.Text) || String.IsNullOrEmpty(moneyBox.Text))
            {
                warning.Visibility = Visibility.Visible;
            }
            else
            if (deleteThis == "Client")
            {
                shoplogic.EnterShop(new Client(double.Parse(moneyBox.Text), nameBox.Text));
                success.Content = "Client successfully added";
            }
            else
            {
                shoplogic.AddToStock(new Product(double.Parse(moneyBox.Text), nameBox.Text));
                success.Content = "Product successfully added";
            }
            moneyBox.Clear();
            nameBox.Clear();
            success.Visibility = Visibility.Visible;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (eventView.Visibility == Visibility.Visible)
            {
                showWarningLabel("You need to be in Client or Product view!");
            }
            else if(clientView.Visibility == Visibility.Visible)
            {
                ClientViewer clientToDelete = (ClientViewer)clientView.SelectedItem;
                shoplogic.LeaveShopp(new Client(0, clientToDelete.Name));
            }
            else
            {
                ProductViewer productToDelete = (ProductViewer)productView.SelectedItem;
                shoplogic.RemoveFromStock(new Product(productToDelete.Price, productToDelete.Name));
            }
            updateLists();
        }

        private void addToBasketButton_Click(object sender, RoutedEventArgs e)
        {
            if (clientView.Visibility == Visibility.Collapsed)
            {
                productView.Visibility = Visibility.Collapsed;
                eventView.Visibility = Visibility.Collapsed;
                clientView.Visibility = Visibility.Visible;
                showWarningLabel("You need to be in Client view!");

            }
            else
            {
                client = (ClientViewer)clientView.SelectedItem;
                clientLabel.Content = client.Name;
                mainPanel.Visibility = Visibility.Collapsed;
                productClientButton.Content = "Add";
                doWhat = "Add";
                productSelectPanel.Visibility = Visibility.Visible;
                updateLists();
            }
        }

        private void productClientButton_Click(object sender, RoutedEventArgs e)
        {
            ProductViewer product = (ProductViewer)productSelectView.SelectedItem;

            for (int i = 0; i < shoplogic.shop.Clients.Count(); i++)
            {
                if (client.Name == shoplogic.shop.Clients[i].Name)
                {
                    if(doWhat == "Add")
                    {
                        for (int j = 0; j < shoplogic.shop.Stock.Count(); j++)
                        {
                            if (product.Name == shoplogic.shop.Stock[j].Name)
                            {
                                shoplogic.AddToBasket(shoplogic.shop.Clients[i], shoplogic.shop.Stock[j]);
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < shoplogic.shop.Clients[i].Basket.Count(); j++)
                        {
                            if (product.Name == shoplogic.shop.Clients[i].Basket[j].Name)
                            {
                                shoplogic.RemoveFromBasket(shoplogic.shop.Clients[i], shoplogic.shop.Clients[i].Basket[j]);
                            }
                        }
                    }
                }
            }
            updateLists();
        }

        private void backClientButton_Click(object sender, RoutedEventArgs e)
        {
            productSelectPanel.Visibility = Visibility.Collapsed;
            mainPanel.Visibility = Visibility.Visible;
        }

        private void deleteFromBasketButton_Click(object sender, RoutedEventArgs e)
        {
            if (clientView.Visibility == Visibility.Collapsed)
            {
                productView.Visibility = Visibility.Collapsed;
                eventView.Visibility = Visibility.Collapsed;
                clientView.Visibility = Visibility.Visible;
                showWarningLabel("You need to be in Client view!");

            }
            else
            {
                client = (ClientViewer)clientView.SelectedItem;
                clientLabel.Content = client.Name;
                mainPanel.Visibility = Visibility.Collapsed;
                productClientButton.Content = "Delete";
                doWhat = "Delete";
                productSelectPanel.Visibility = Visibility.Visible;
                updateLists();
            }
        }

        private void CharacterValidationText(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^A-Za-z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NumberValidationText(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+?$");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void updateLists()
        {
            warningLabel.Visibility = Visibility.Collapsed;
            List<ProductViewer> products = new List<ProductViewer>();
            for (int i = 0; i < shoplogic.shop.Stock.Count(); i++)
            {
                products.Add(new ProductViewer() { Name = shoplogic.shop.Stock[i].Name, Quantity = 1, Price = shoplogic.shop.Stock[i].Price });
            }
            productView.ItemsSource = products;
            List<ProductViewer> products2 = new List<ProductViewer>();
            if (doWhat == "Delete")
            {
                for (int i = 0; i < shoplogic.shop.Clients.Count(); i++)
                {
                    if (client.Name == shoplogic.shop.Clients[i].Name)
                    {
                        for (int j = 0; j < shoplogic.shop.Clients[i].Basket.Count(); j++)
                        {
                            products2.Add(new ProductViewer() { Name = shoplogic.shop.Clients[i].Basket[j].Name, Quantity = 1, Price = shoplogic.shop.Clients[i].Basket[j].Price });
                        }
                    }
                }
                productSelectView.ItemsSource = products2;
            }
            else
            {
                productSelectView.ItemsSource = products;
            }

            List<ClientViewer> clients = new List<ClientViewer>();
            for (int i = 0; i < shoplogic.shop.Clients.Count(); i++)
            {
                clients.Add(new ClientViewer() { Name = shoplogic.shop.Clients[i].Name, Balance = shoplogic.shop.Clients[i].Money, BasketValue = shoplogic.ValueOfBasket(shoplogic.shop.Clients[i]) });
            }
            clientView.ItemsSource = clients;

            List<EventViewer> events = new List<EventViewer>();
            for (int i = 0; i < shoplogic.shop.Events.Count(); i++)
            {
                events.Add(new EventViewer() { Name = shoplogic.shop.Events[i].Name, Value = "nothing" });
            }
            eventView.ItemsSource = events;
        }

        private void showWarningLabel(string message)
        {
            warningLabel.Content = message;
            warningLabel.Visibility = Visibility.Visible;
        }

        public class ProductViewer
        {
            public string Name { get; set; }

            public int Quantity { get; set; }

            public double Price { get; set; }
        }

        public class ClientViewer
        {
            public string Name { get; set; }

            public double Balance { get; set; }

            public double BasketValue { get; set; }
        }

        public class EventViewer
        {
            public string Name { get; set; }

            public string Value { get; set; }
        }

    }
}
