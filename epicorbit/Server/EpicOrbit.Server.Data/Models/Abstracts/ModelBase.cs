using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Server.Data.Repositories.Attributes;

namespace EpicOrbit.Server.Data.Models.Abstracts {
    public abstract class ModelBase {

        // index the creationdate / timestamp for time tracking / analysis
        [Index(false)] public virtual DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
