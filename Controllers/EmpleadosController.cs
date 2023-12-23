using AppNomina.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppNomina.Controllers
{
    public class EmpleadosController : Controller
    {
        private DbContextAppNomina dbContext = null;

        public EmpleadosController(DbContextAppNomina pContext)
        {
            dbContext = pContext;
        }

        public IActionResult Index()
        {
            return View(dbContext.Empleados.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Empleados pEmpleados)
        {
            if (pEmpleados == null)
            {
                return NotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    // Calcula los valores antes de guardar en la base de datos
                    pEmpleados.SalarioBruto = (pEmpleados.HNormales * 1800) + (pEmpleados.HExtras * 2700);
                    pEmpleados.Deducciones = CalcularDeducciones(pEmpleados.SalarioBruto);
                    pEmpleados.SalarioNeto = pEmpleados.SalarioBruto - pEmpleados.Deducciones;

                    // Establece un valor para FechaRegistro
                    pEmpleados.FechaRegistro = DateTime.Now;

                    // Otros campos ya se llenarán automáticamente en la vista
                    dbContext.Empleados.Add(pEmpleados);
                    dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }


        [HttpGet]
        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            Empleados empleado = dbContext.Empleados.FirstOrDefault(e => e.ID == Id);

            if (empleado == null)
            {
                return NotFound();
            }
            else
            {
                return View(empleado);
            }
        }


        [HttpGet]
        public IActionResult Edit(int? Id)
        { if (Id == null) 
            {
                return NotFound();
            }
         var temp = dbContext.Empleados.FirstOrDefault(x => x.ID == Id);
            if(temp == null) 
            {
                return NotFound();
            }
            else
            {
                return View(temp);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, [Bind] Empleados pEmpleados) 
        {
            if (pEmpleados == null)
            {
                return NotFound();
            }

            if (Id != pEmpleados.ID) 
            {
                return NotFound();
            }
            var temp = dbContext.Empleados.FirstOrDefault(x => x.ID == Id);

            if (temp == null)
            {
                return NotFound();
            }
            else 
            {
                temp.NombreCompleto = pEmpleados.NombreCompleto;
                temp.HNormales = pEmpleados.HNormales;
                temp.HExtras = pEmpleados.HExtras;
                temp.SalarioBruto = pEmpleados.SalarioBruto;
                temp.SalarioNeto = pEmpleados.SalarioNeto;
                temp.Deducciones = pEmpleados.Deducciones;
                //temp.FechaRegistro = pEmpleados.FechaRegistro;

                dbContext.Empleados.Update(temp);

                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Delete(int? Id) 
        {
            if (Id == null)
            {
                return NotFound();
            }
            var temp = dbContext.Empleados.FirstOrDefault(x => x.ID == Id);

            if(temp == null)
            {
                return NotFound();
            }
            else
            {
                return View(temp);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([Bind] Empleados pEmpleados)
        {
            if(pEmpleados == null)
            {
                return NotFound();
            }

            var temp = dbContext.Empleados.FirstOrDefault(x => x.ID == pEmpleados.ID);
            if(temp == null)
            {
                return NotFound();
            }
            else
            {
                dbContext.Empleados.Remove(temp);
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

        }
        private decimal CalcularDeducciones(decimal salarioBruto)
        {
            // Tu lógica para calcular deducciones basadas en las condiciones proporcionadas
            if (salarioBruto <= 250000)
            {
                return salarioBruto * 0.09m;
            }
            else if (salarioBruto <= 380000)
            {
                return salarioBruto * 0.12m;
            }
            else
            {
                return salarioBruto * 0.15m;
            }
        }
    }
}
