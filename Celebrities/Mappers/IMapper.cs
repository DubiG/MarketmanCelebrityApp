using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celebrities.Mappers
{
    public interface IMapper<E>
    {
        E Map(HtmlDocument htmlDocument);
        List<E> MapAsList(HtmlDocument htmlDocument);
    }
}
