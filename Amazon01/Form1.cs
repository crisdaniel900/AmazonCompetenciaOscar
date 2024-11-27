using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Windows.Forms;

namespace AmazonPriceChecker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // Botón para obtener el precio
        private async void btnGetPrice_Click(object sender, EventArgs e)
        {
            // Validar que se haya seleccionado un elemento
            if (lstLinks.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un producto del listado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Intentar convertir el elemento seleccionado a ListItem
            var selectedItem = lstLinks.SelectedItem as ListItem;

            if (selectedItem == null)
            {
                MessageBox.Show("Error al procesar el elemento seleccionado. Intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar que el URL sea válido
            string url = selectedItem.Url;

            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("El enlace guardado no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Obtener el precio
            string price = await GetAmazonPrice(url);

            if (!string.IsNullOrEmpty(price))
            {
                lblResult.Text = $"El precio actual es: {price}";
            }
            else
            {
                lblResult.Text = "No se pudo obtener el precio.";
            }
        }

        // Botón para guardar un enlace
        private async void btnSaveLink_Click(object sender, EventArgs e)
        {
            string url = txtUrl.Text.Trim();

            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Por favor, ingrese un URL válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string productName = await GetProductName(url);

            if (!string.IsNullOrEmpty(productName))
            {
                if (!lstLinks.Items.Contains(productName))
                {
                    lstLinks.Items.Add(new ListItem { Name = productName, Url = url });
                    txtUrl.Clear();
                    MessageBox.Show("Producto guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("El producto ya está en la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("No se pudo obtener el nombre del producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para obtener el precio desde Amazon
        private async Task<string> GetAmazonPrice(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0.0.0 Safari/537.36");

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string pageContents = await response.Content.ReadAsStringAsync();

                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(pageContents);

                        var priceWhole = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'a-price-whole')]");
                        var priceFraction = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'a-price-fraction')]");

                        if (priceWhole != null && priceFraction != null)
                        {
                            return $"{priceWhole.InnerText.Trim()}.{priceFraction.InnerText.Trim()}";
                        }
                        else if (priceWhole != null)
                        {
                            return priceWhole.InnerText.Trim();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener el precio: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return null;
        }

        // Método para obtener el nombre del producto desde Amazon
        private async Task<string> GetProductName(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0.0.0 Safari/537.36");

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string pageContents = await response.Content.ReadAsStringAsync();

                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(pageContents);

                        var titleNode = doc.DocumentNode.SelectSingleNode("//span[@id='productTitle']");
                        if (titleNode != null)
                        {
                            return titleNode.InnerText.Trim();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener el nombre del producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return null;
        }
    }

    // Clase para almacenar información del producto
    public class ListItem
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public override string ToString()
        {
            return Name; // Mostrar solo el nombre en el ListBox
        }
    }
}
