using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingKsDataLayer.Pocos
{
  public class Food
  {
    public Food()
    {
      Measures = new List<Measure>();
    }

    public int Id { get; set; }
    public string Description { get; set; }

    public virtual ICollection<Measure> Measures { get; set; }
  }
}
