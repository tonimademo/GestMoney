using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace GestMoney.Clases
{
    class Funciones
    {
        public static bool ExisteEnTabla(string tabla, string condicion)
        {
            SqlCommand command;
            bool result;

            try
            {
                command = new SqlCommand("select id from " + tabla + " where " + condicion, SQLConecction.conn);

                SqlDataReader reader = command.ExecuteReader();
                
                if (reader.HasRows)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                reader.Close();
                return result;
                
            }catch (Exception e){
                return false;
            }
        }

        public static void ModificarFiltro(ref DataTable table, string filtro, string condicion, string regex)
        {
            
            try
            {
                
                MatchCollection matches = Regex.Matches(filtro, regex, RegexOptions.IgnorePatternWhitespace);
         
                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                        filtro = filtro.Replace(match.Value, condicion);
                }
                else
                {
                    filtro += condicion;
                }


                table.DefaultView.RowFilter = filtro;

            }
            catch (Exception e)
            {
            }
        }
    }

    public class ComboItem
    {
        public string Texto { get; set; }
        public object Valor { get; set; }

        public ComboItem(string name, int value)
        {
            Texto = name; Valor = value;
        }

        public override string ToString()
        {
            return Texto;
        }
    }
}
