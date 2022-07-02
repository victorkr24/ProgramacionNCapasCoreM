using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public  class Usuario
    {
        public static ML.Result AddEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LEscogidoProgramacionNCapasMayoContext context = new DL.LEscogidoProgramacionNCapasMayoContext())
                {
                    //var query = context.Database.ExecuteSqlRaw($"AseguradoraAdd '{aseguradora.Nombre}',{aseguradora.Usuario.IdUsuario}");
                      var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}','{usuario.UserName}','{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}','{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.FechaNacimiento}', '{usuario.CURP}', {usuario.Rol.IdRol}");


                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertó el registro";
                    }

                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        //UPDATE

        public static ML.Result UpdateEF(ML.Usuario usuario) //12/12/2019 No se elimina la credencial, se cambia
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL.LEscogidoProgramacionNCapasMayoContext context = new DL.LEscogidoProgramacionNCapasMayoContext())
                {
                    //var updateResult = context.UsuarioUpdate(usuario.IdUsuario, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno,
                    //    usuario.Email, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.FechaNacimiento, usuario.CURP, usuario.Rol.IdRol);
                    var query = context.Database.ExecuteSqlRaw($"UsuarioUpdate  {usuario.IdUsuario},'{usuario.Nombre}','{usuario.UserName}','{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}','{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.FechaNacimiento}', '{usuario.CURP}', {usuario.Rol.IdRol}");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizó el status de la credencial";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        //DELETE
        public static ML.Result DeleteEF(ML.Usuario usuario) //12/12/2019 No se elimina la credencial, se cambia
        {
            ML.Result result = new ML.Result();
            try
            {


                using (DL.LEscogidoProgramacionNCapasMayoContext context = new DL.LEscogidoProgramacionNCapasMayoContext())
                {
                   // var updateResult = context.UsuarioDelete(usuario.IdUsuario);
                        var query = context.Database.ExecuteSqlRaw($"UsuarioDelete {usuario.IdUsuario}");
                    
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizó el status de la credencial";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        //GETALL

        public static ML.Result UsuarioGetAllEF()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LEscogidoProgramacionNCapasMayoContext context = new DL.LEscogidoProgramacionNCapasMayoContext())
                {

                   // var aseguradoras = context.Aseguradoras.FromSqlRaw($"AseguradoraGetAll").ToList();
                    var usuarios = context.Usuarios.FromSqlRaw($"UsuariosGetAll").ToList();

                    result.Objects = new List<object>();

                    if (usuarios != null)
                    {
                        foreach (var obj in usuarios)
                        {
                            ML.Usuario usuario = new ML.Usuario();

                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.UserName = obj.UserName;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Email = obj.Email;
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            usuario.Celular = obj.Celular;
                            usuario.FechaNacimiento = obj.FechaNacimiento.ToString();
                            usuario.CURP = obj.Curp;

                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = obj.IdRol.Value;
                            usuario.Rol.Nombre = obj.RolNombre;

                            result.Objects.Add(usuario);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }
        public static ML.Result GetByIdEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LEscogidoProgramacionNCapasMayoContext context = new DL.LEscogidoProgramacionNCapasMayoContext())
                {
                    //var objDepartamento = context.DepartamentoGetById(IdDepartamento).FirstOrDefault();
                    //var objusuarios = context..UsuarioGetById(IdUsuario).FirstOrDefault();
                    var objusuarios = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();

                    result.Objects = new List<object>();

                    if (objusuarios != null)
                    {

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = objusuarios.IdUsuario;
                        usuario.Nombre = objusuarios.Nombre;
                        usuario.UserName = objusuarios.UserName;
                        usuario.ApellidoPaterno = objusuarios.ApellidoPaterno;
                        usuario.ApellidoMaterno = objusuarios.ApellidoMaterno;
                        usuario.Email = objusuarios.Email;
                        usuario.Sexo = objusuarios.Sexo;
                        usuario.Telefono = objusuarios.Telefono;
                        usuario.Celular = objusuarios.Celular;
                        usuario.FechaNacimiento = objusuarios.FechaNacimiento.ToString();
                        usuario.CURP = objusuarios.Curp;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = objusuarios.IdRol.Value;
                        usuario.Rol.Nombre = objusuarios.RolNombre;


                        result.Object = usuario;


                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Departamento";
                    }

                }


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }


    }
}
