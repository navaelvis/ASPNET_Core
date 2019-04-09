using Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;

namespace WebAPIXamarinForm
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnBuscarProductos_Clicked(object sender, EventArgs e)
        {
            int _idCategoria = int.Parse(txtIdCategoria.Text);
            
            try
            {
                //// busca el nombre de la categoría
                string _requestURI = @"http://172.16.5.220:51038/categorias/obtenernombredecategoria/" + _idCategoria.ToString();
                using (var _cliente = new HttpClient())
                {
                    _cliente.DefaultRequestHeaders.Accept.Clear();
                    _cliente.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    var _resultadoJson = await _cliente.GetStringAsync(_requestURI);
                    var _resultadoDeserializado = JsonConvert.DeserializeObject(_resultadoJson);
                    if (_resultadoDeserializado != null)
                        lblCategoria.Text = _resultadoDeserializado.ToString();
                }

                //// busca los productos asociados a la categoría seleccionada
                _requestURI = @"http://172.16.5.220:51038/productos/obtenerproductosporidcategoria/" + _idCategoria.ToString();
                using (var _cliente = new HttpClient())
                {
                    _cliente.DefaultRequestHeaders.Accept.Clear();
                    _cliente.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    var _resultadoJson = await _cliente.GetStringAsync(_requestURI);
                    var _resultadoDeserializado = JsonConvert.DeserializeObject<List<Products>>(_resultadoJson);
                    if (_resultadoDeserializado != null)
                        lstProductos.ItemsSource = _resultadoDeserializado;
                }

                lblInfo.Text = "";
            }
            catch (Exception pEx)
            {
                lblInfo.Text = pEx.Message;
                lblInfo.TextColor = Color.Red;
            }
        }

        private void TxtIdCategoria_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblCategoria.Text = "";
            lstProductos.ItemsSource = null;
        }
    }
}
