using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Parcial1_1AP1.Entidades;
using Parcial1_1AP1.BLL;

namespace Parcial1_1AP1.UI.Registros
{
    public partial class rEestudiante : Form
    {
        public rEestudiante()
        {
            InitializeComponent();
        }

        private void Limpiar()
        {
            IDnumericUpDown.Value = 0;
            EstudiantetextBox.Text = string.Empty;
            FechadateTimePicker.Value = DateTime.Now;
            ValortextBox.Text = string.Empty;
            LogradotextBox.Text = string.Empty;
            PerdidotextBox.Text = string.Empty;
            PronosticocomboBox.Text = string.Empty;
            MyerrorProvider.Clear();
        }

        private Estudiantes LlenaClase()
        {
            Estudiantes evaluacion = new Estudiantes();

            evaluacion.IdEvaluacion = (int)IDnumericUpDown.Value;
            evaluacion.Fecha = FechadateTimePicker.Value;
            evaluacion.Estudiante = EstudiantetextBox.Text;
            evaluacion.Valor = Convert.ToDecimal(ValortextBox.Text);
            evaluacion.Logrado = Convert.ToDecimal(LogradotextBox.Text);
            evaluacion.Perdido = evaluacion.Valor - evaluacion.Logrado;
            evaluacion.Pronostico = PronosticocomboBox.SelectedIndex;

            return evaluacion;
        }

        private void LlenaCampo(Estudiantes evaluacion)
        {
            IDnumericUpDown.Value = evaluacion.IdEvaluacion;
            FechadateTimePicker.Value = evaluacion.Fecha;
            EstudiantetextBox.Text = evaluacion.Estudiante;
            ValortextBox.Text = Convert.ToString(evaluacion.Valor);
            LogradotextBox.Text = Convert.ToString(evaluacion.Logrado);
            PerdidotextBox.Text = Convert.ToString(evaluacion.Perdido);
            PronosticocomboBox.SelectedIndex = evaluacion.Pronostico;
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Estudiantes evaluacion = EstudiantesBLL.Buscar((int)IDnumericUpDown.Value);
            return (evaluacion != null);
        }

        private bool Validar()
        {
            bool paso = true;

            if (string.IsNullOrWhiteSpace(Convert.ToString(FechadateTimePicker.Value)))
            {
                MyerrorProvider.SetError(FechadateTimePicker, "El campo Fecha no puede estar vacio");
                FechadateTimePicker.Focus();
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(EstudiantetextBox.Text))
            {
                MyerrorProvider.SetError(EstudiantetextBox, "El campo Estudiante no puede estar vacio");
                EstudiantetextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(ValortextBox.Text))
            {
                MyerrorProvider.SetError(ValortextBox, "El campo Valor no puede estar vacio");
                ValortextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(LogradotextBox.Text))
            {
                MyerrorProvider.SetError(LogradotextBox, "El campo Logrado no puede estar vacio");
                LogradotextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(Convert.ToString(PronosticocomboBox.SelectedIndex)))
            {
                MyerrorProvider.SetError(PronosticocomboBox, "El campo Pronostico no puede estar vacio");
                PronosticocomboBox.Focus();
                paso = false;
            }


            if (Convert.ToDecimal(ValortextBox.Text) < 0 || Convert.ToDecimal(ValortextBox.Text) > 100)
            {
                MyerrorProvider.SetError(ValortextBox, "El campo Valor no puede tener un valor menor que 0 ni mayor que 100");
                ValortextBox.Focus();
                paso = false;
            }

            if (Convert.ToDecimal(LogradotextBox.Text) < 0 || Convert.ToDecimal(LogradotextBox.Text) > 100)
            {
                MyerrorProvider.SetError(LogradotextBox, "El campo Logrado no puede tener un valor menor que 0 ni mayor que 100");
                LogradotextBox.Focus();
                paso = false;
            }

            return paso;
        }




        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            bool paso = false;
            Estudiantes evaluacion = new Estudiantes();

            if (!Validar())
                return;

            evaluacion = LlenaClase();

            if (IDnumericUpDown.Value == 0)
                paso = EstudiantesBLL.Guardar(evaluacion);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar un registro que no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = EstudiantesBLL.Modificar(evaluacion);
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado", "Existe", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No Guardado", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            MyerrorProvider.Clear();
            int id;
            int.TryParse(Convert.ToString(IDnumericUpDown.Value), out id);

            Limpiar();

            if (EstudiantesBLL.Eliminar(id))
            {
                MessageBox.Show("Registro eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MyerrorProvider.SetError(IDnumericUpDown, "No se pudo eliminar el registro");
            }
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            int id;
            int.TryParse(Convert.ToString(IDnumericUpDown.Value), out id);

            Limpiar();

            Estudiantes evaluacion = EstudiantesBLL.Buscar(id);

            if (evaluacion != null)
            {
                LlenaCampo(evaluacion);
            }
            else
            {
                MessageBox.Show("El registro que desea buscar no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LogradotextBox_TextChanged(object sender, EventArgs e)
        {
            decimal Resultado = 0;
            decimal valor = 0;
            decimal logrado = 0;

            if (!string.IsNullOrWhiteSpace(ValortextBox.Text))
                valor = Convert.ToDecimal(ValortextBox.Text);
            if (!string.IsNullOrWhiteSpace(LogradotextBox.Text))
                logrado = Convert.ToDecimal(LogradotextBox.Text);

            Resultado = valor - logrado;
            PerdidotextBox.Text = Resultado.ToString();

            if (Resultado < 25)
            {
                PronosticocomboBox.SelectedIndex = 0;
            }

            if (Resultado >= 25 && Resultado <= 30)
            {
                PronosticocomboBox.SelectedIndex = 1;
            }

            if (Resultado < 30)
            {
                PronosticocomboBox.SelectedIndex = 2;
            }
        }

        private void ValortextBox_TextChanged(object sender, EventArgs e)
        {
            decimal Resultado = 0;
            decimal valor = 0;
            decimal logrado = 0;

            if (!string.IsNullOrWhiteSpace(ValortextBox.Text))
                valor = Convert.ToDecimal(ValortextBox.Text);
            if (!string.IsNullOrWhiteSpace(LogradotextBox.Text))
                logrado = Convert.ToDecimal(LogradotextBox.Text);

            Resultado = valor - logrado;
            PerdidotextBox.Text = Resultado.ToString();

            if (Resultado < 25)
            {
                PronosticocomboBox.SelectedIndex = 0;
            }

            if (Resultado >= 25 && Resultado <= 30)
            {
                PronosticocomboBox.SelectedIndex = 1;
            }

            if (Resultado < 30)
            {
                PronosticocomboBox.SelectedIndex = 2;
            }

        }

  
    }
}
