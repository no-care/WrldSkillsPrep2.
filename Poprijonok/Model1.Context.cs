//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Poprijonok
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PoprijonokEntities : DbContext
    {
        private static PoprijonokEntities _context;
        public PoprijonokEntities()
            : base("name=PoprijonokEntities")
        {
        }
        public static PoprijonokEntities GetContext() 
        {
            if (_context == null)
                _context = new PoprijonokEntities();
            return _context;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Agent_address> Agent_address { get; set; }
        public virtual DbSet<Agent_release_history> Agent_release_history { get; set; }
        public virtual DbSet<Agents> Agents { get; set; }
        public virtual DbSet<Agents_type> Agents_type { get; set; }
        public virtual DbSet<Cex> Cex { get; set; }
        public virtual DbSet<Employee_pasport> Employee_pasport { get; set; }
        public virtual DbSet<Employes> Employes { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<material_type> material_type { get; set; }
        public virtual DbSet<Materials> Materials { get; set; }
        public virtual DbSet<Materials_history> Materials_history { get; set; }
        public virtual DbSet<Minimal_price_history_change> Minimal_price_history_change { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Priority_history> Priority_history { get; set; }
        public virtual DbSet<Product_info> Product_info { get; set; }
        public virtual DbSet<Product_sale> Product_sale { get; set; }
        public virtual DbSet<Product_size> Product_size { get; set; }
        public virtual DbSet<Product_type> Product_type { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Standart_types> Standart_types { get; set; }
        public virtual DbSet<Supplier_type> Supplier_type { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
