using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppCandidato.Models;

namespace WebAppCandidato
{
    public partial class _Default : Page
    {

        List<Clientes> cli;
        List<Clientes> Tmpcli;
        protected void Page_Load(object sender, EventArgs e)
        {



            btnCarga.Click += BtnCarga_Click;
            if (!IsPostBack)
            {
                lblHora.Text = "El sistema corrio por primera vez a las " + DateTime.Now.ToString();
                Session["cli"] = null;
            }
            else
            {

                GetData();
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {

        }
        private void GetData()
        {
            cli = new List<Clientes>();
            var HostURI = "https://pos.dermalia.mx/webforms/data";

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(HostURI);
            request.Method = "GET";
            String dataJson = String.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                dataJson = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
            }

            var dataObject = JsonConvert.DeserializeObject<List<Clientes>>(dataJson);
            cli = dataObject;

            if (Session["cli"] == null)
            {
                Session["cli"] = dataJson;

            }

            foreach (var item in dataObject)
            {
                TableRow tr = new TableRow();
                var celNombre = new TableCell();
                celNombre.Text = item.nombres;
                var celMPrimer = new TableCell();
                celMPrimer.Text = item.primerApellido;
                var celSegundo = new TableCell();
                celSegundo.Text = item.segundoApellido;
                var sexo = new TableCell();

                DropDownList dd = new DropDownList();
                dd.SelectedIndexChanged += Dd_SelectedIndexChanged;
                dd.Attributes["ID"] = item.idCliente.ToString();

                var M = new ListItem();
                M.Text = "Masculino";
                // M.Value = JsonConvert.SerializeObject(item);


                var F = new ListItem();
                F.Text = "Femenino";
                //F.Value = JsonConvert.SerializeObject(item);


                dd.Items.Add(M);
                dd.Items.Add(F);
                sexo.Controls.Add(dd);

                dd.AutoPostBack = true;

                dd.ViewStateMode = ViewStateMode.Enabled;
                dd.EnableViewState = true;


                //dd.AutoPostBack = true;
                if (item.sexo.ToUpper() == "M")
                {
                    dd.SelectedIndex = 0;

                }
                else
                {
                    dd.SelectedIndex = 1;
                }


                tr.Cells.Add(celNombre);
                tr.Cells.Add(celMPrimer);
                tr.Cells.Add(celSegundo);
                tr.Cells.Add(sexo);

                tblClientes.ViewStateMode = ViewStateMode.Enabled;
                tblClientes.EnableViewState = true;
                tblClientes.Rows.Add(tr);

            }





        }
        private void BtnCarga_Click(object sender, EventArgs e)
        {

            GetData();

        }

        private void Dd_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList itemSelected = ((DropDownList)sender);

      
                Tmpcli = JsonConvert.DeserializeObject<List<Clientes>>(Session["cli"].ToString()); 
          
            var cliente = Tmpcli.Where(a => a.idCliente == int.Parse(itemSelected.Attributes["ID"])).FirstOrDefault();

            if (itemSelected.Text.ToUpper().Contains("MASCU"))
            {
                Tmpcli.ForEach(a =>
                {

                    if (a.idCliente == cliente.idCliente)
                    {
                        a.sexo = "M";
                    }
                });

            }
            else
            {
                Tmpcli.ForEach(a =>
                {

                    if (a.idCliente == cliente.idCliente)
                    {
                        a.sexo = "F";
                    }
                });

            }

            Session["cli"] = JsonConvert.SerializeObject(Tmpcli);

            var PorcF = Tmpcli.Where(a => a.sexo == "F").Count();
            var PorcM = Tmpcli.Where(a => a.sexo == "M").Count();
            var TotalC = Tmpcli.Count();

            var PorcentajeM = (float)((float)PorcM / (float)TotalC) * 100;
            var PorcentajeF = (float)((float)PorcF / (float)TotalC) * 100;

           
            tblInfo.Controls.Clear();
            TableRow tr = new TableRow();

            TableCell tb1 = new TableCell();


            tb1.Text = "Registro Modificado:";
            if (itemSelected.Text.ToUpper().Contains("MASCU"))
            {
                tblInfo.BackColor = System.Drawing.Color.AliceBlue;
            }
            else
            {
                tblInfo.BackColor = System.Drawing.Color.LightPink;
            }


            TableCell tb2 = new TableCell();

            tb2.Text = cliente.nombres + " " + cliente.primerApellido + " " + cliente.segundoApellido + " (" + (itemSelected.Text.ToUpper().Contains("MASCU") ? "M" : "F") + ")";

            TableCell tb3 = new TableCell();

            tb3.Text = "M:" + PorcentajeM.ToString("0.0000") + "%" + "F:" + PorcentajeF.ToString("0.0000") + "%"; ;

            tr.Cells.Add(tb1);
            tr.Cells.Add(tb2);
            tr.Cells.Add(tb3);
            tblInfo.Controls.Add(tr);


        }
    }
}