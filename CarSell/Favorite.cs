//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarSell
{
    using System;
    using System.Collections.Generic;
    
    public partial class Favorite
    {
        public int Id { get; set; }
        public Nullable<int> IdCar { get; set; }
        public Nullable<int> IdAccount { get; set; }
    
        public virtual Account account { get; set; }
        public virtual Car car { get; set; }
    }
}
