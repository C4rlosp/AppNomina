using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Drawing.Text;
using AppNomina.Models;
using Microsoft.AspNetCore.Identity;

namespace AppNomina.Controllers
{
    public class UsuariosController : Controller
    {
        
        private DbContextAppNomina dbContext;

        /// <summary>
        /// Constructor con los parametros del controlador
        /// </summary>
        /// <param name="pContext"></param>
        public UsuariosController(DbContextAppNomina pContext) 
        {
            dbContext = pContext;
        }

        /// <summary>
        /// Metodo encargado de mostrar el formulurio de autenticacion
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind] Usuarios pUser)
        {
            if (pUser == null)
            {
                return NotFound();
            }
            else
            {
                var tempUser = dbContext.Usuarios.FirstOrDefault(x => x.Email == pUser.Email);
                if (tempUser == null)
                {
                    TempData["ErrorMessage"] = "El usuario no existe.";
                    return RedirectToAction("Login");
                }
                else
                {
                    if (tempUser.Password.Equals(pUser.Password))
                    {
                        var userClaims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name, tempUser.NombreCompleto),
                             new Claim(ClaimTypes.Name, pUser.Email),


                        };
                        var granmaIdentity = new ClaimsIdentity(userClaims, "User Identity");
                        var userPrincipal = new ClaimsPrincipal(new[] { granmaIdentity });

                        HttpContext.SignInAsync(userPrincipal);

                        return RedirectToAction("Index", "Empleados");

                    }
                    else
                    {
                        // Contraseña incorrecta, agregar mensaje TempData
                        TempData["ErrorMessage"] = "Contraseña incorrecta. Inténtalo de nuevo.";
                        return RedirectToAction("Login");
                    }
                }
            }
        }//cierre IActionResult

        //Método logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }

}

