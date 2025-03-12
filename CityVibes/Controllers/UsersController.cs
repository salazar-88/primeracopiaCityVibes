using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using CityVibes.Models;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;  
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace CityVibes.Controllers
{
    public class UsersController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/Users
        [HttpGet]
        [Route("api/Users")]

        public List<User> GetUsers()
        {
            System.Diagnostics.Debug.WriteLine("Entrando al método GetUsers");

            var users = db.Users.ToList();  // Recupera todos los usuarios

            if (users == null || !users.Any())
            {
                System.Diagnostics.Debug.WriteLine("No hay usuarios en la base de datos");
                throw new Exception("No hay usuarios en la base de datos");
            }

            return db.Users.ToList();
        }

        // GET: api/Users/5
        [HttpGet]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut]
        [ResponseType(typeof(void))]
        [Route("api/users")]
        public IHttpActionResult PutUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!db.Users.Any(x => x.Id_User == user.Id_User))
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [HttpPost]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.Id_User }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete]
        [Route("api/users/Delete/")]
        public IHttpActionResult DeleteUser(User user)
        {
            User userDelete = db.Users.Find(user.Id_User);
            if (userDelete == null)
            {
                return NotFound();
            }

            db.Users.Remove(userDelete);
            db.SaveChanges();

            return Ok(userDelete);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        //ver la lista de usuarios en orden descendente
        [HttpGet]
        [Route("api/Users/OrderDesc")]
        public List<User> OrderDesc()
        {
            return db.Users.OrderByDescending(x => x.Creation_Date).ToList();
        }

        // ver solo los nombres de usuarios
        [HttpGet]
        [Route("api/Users/Usernames")]
        public List<string> Usernames()
        {
            return db.Users.Select(x => x.Username).ToList();
        }

        //Ver solo el correo de los usuarios
        [HttpGet]
        [Route("api/Users/Emails")]
        public string[] EmailList()
        {
            return db.Users.Select(x => x.Email).ToArray();
        }

        //verificar los usuarios habilitados en el sistema

        [HttpGet]
        [Route("api/Users/Availables")]
        public List<User> UserAvaibles()
        {
            return db.Users.Where(x => x.Status == true).ToList();
        }

       

        //registrar un nuevo usuario en la base de datos
        [HttpPost]
        [Route("api/Users/Register")]
        public User Register(User user)
        {

            db.Users.Add(user);
            db.SaveChanges();
            return user;
        }

        // desabilitar usuarios
        [HttpPost]
        [Route("api/Users/Unable")]
        public User Unable(User user)
        {
            user = db.Users.Find(user.Id_User);
            user.Status = false;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return user;
        }

        //login de un usuario

        [HttpPost]
        [Route("api/Users/Login")]
        public IHttpActionResult Login(User user)
        {
            if (db.Users.Any(x => x.Email == user.Email && x.Password == user.Password)) 
            {

                return Json(new { Success = true, Value = user.Email, Message = "El login se valido correctamente" });
             }
            else
            {
                return Json(new { Success = false, Value = user.Email, Message = "Usuario o contraseña incorrectos" });

             }
        }


        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id_User == id) > 0;
        }
    }
}

