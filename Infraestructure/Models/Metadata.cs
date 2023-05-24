using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    internal partial class ResidenciaMetadata
    {
        [Display(Name = "Numero de Residencia")]
        public int ID { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int UsuarioID { get; set; }

        [Display(Name ="Cantidad de Personas")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int CantPersonas { get; set; }

        [Display(Name = "Fecha de Inicio")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Estado { get; set; }

        [Display(Name = "Cantidad de Carros")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int CantidadCarros { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstadoCuenta> EstadoCuenta { get; set; }
        public virtual Usuario Usuario { get; set; }
    }

    internal partial class EstadoCuentaMetadata
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int ResidenciaID { get; set; }

        [Display(Name = "Plan de Cobro")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int PlanCobroID { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public double Monto { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Estado { get; set; }
        public System.DateTime Mes { get; set; }

        public virtual PlanCobro PlanCobro { get; set; }
        public virtual Residencia Residencia { get; set; }
    }

    internal partial class PlanCobroMetadata
    {
        public int ID { get; set; }

        [StringLength(50, MinimumLength = 5, ErrorMessage = "{0}La descripcion debe tener como minimo 50 caracteres")]
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Descripcion { get; set; }

        [Display(Name = "Costo por mes")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessage = "{0} acepta solo números con dos decimales")]
        public decimal TotalMes { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstadoCuenta> EstadoCuenta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        
        [Display(Name = "Rubros de Cobro")]
        public virtual ICollection<RubroCobro> RubroCobro { get; set; }
    }

    internal partial class RubroCobroMetadata {
        public int ID { get; set; }

        [StringLength(50, MinimumLength = 5, ErrorMessage = "{0}La descripcion debe tener como minimo 5 y máximo 50 caracteres")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; }

        [StringLength(1000, MinimumLength = 5, ErrorMessage = "{0}La descripcion debe tener como minimo 5 caracteres")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Descripcion { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessage = "{0} acepta solo números con dos decimales")]
        public decimal Costo { get; set; }
        public int Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlanCobro> PlanCobro { get; set; }
    }

    internal partial class UsuarioMetadata
    {
        public int UsuarioID { get; set; }

        [Display(Name = "Tipo de Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int TipoUsuarioID { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0}La descripcion debe tener como minimo 3 y máximo 50 caracteres")]
        public string Nombre { get; set; }

        [Display(Name = "Primer Apellido")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "{0}La descripcion debe tener como minimo 5 y máximo 50 caracteres")]
        public string Apellido1 { get; set; }

        [Display(Name = "Segundo Apellido")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "{0}La descripcion debe tener como minimo 5 y máximo 50 caracteres")]
        public string Apellido2 { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [RegularExpression("^[_A-Za-z0-9-]+(.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(.[A-Za-z0-9-]+)*(.[A-Za-z]{2,4})$", ErrorMessage = "El Formato Del Correo Electrónico Es Incorrecto.Ejemplo: Correo @Dominio.Com")]

        public string Correo { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "{0}La descripcion debe tener como minimo 3 caracteres")]
        public string Contrasenna { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Incidencia> Incidencia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Informacion> Informacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservacion> Reservacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Residencia> Residencia { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }
    }

    internal partial class InformacionMetadata
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int UsuarioID { get; set; }

        
        [Display(Name = "Título")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Titulo { get; set; }

        [StringLength(1000, MinimumLength = 10, ErrorMessage = "{0}La descripcion debe tener como minimo 50 caracteres")]
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Informacion1 { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime Fecha { get; set; }
        public virtual Usuario Usuario { get; set; }
    }

    internal partial class IncidenciaMetadata
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int UsuarioID { get; set; }

        [StringLength(1000, MinimumLength = 4,ErrorMessage = "{0}La descripcion debe tener como minimo 4 caracteres")]
        [Display(Name = "Título")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Titulo { get; set; }

        [StringLength(1000, MinimumLength = 10, ErrorMessage = "{0}La descripcion debe tener como minimo 50 caracteres")]
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Informacion { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime Fecha { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Estado { get; set; }
        public virtual Usuario Usuario { get; set; }
    }

    internal partial class ReservacionMetadata
    {
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int AreaComunID { get; set; }
        public int UsuarioID { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime Fecha { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Estado { get; set; }

        public virtual AreaComun AreaComun { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
