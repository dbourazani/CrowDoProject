using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowDo.Options
{
    public enum ProjectCategory
    {
        Invalid = 0,
        Art = 1,
        Comics = 2,
        Design = 3,
        Fashion = 4,
        Games = 5,
        Journalism = 6,
        Music = 7,
        Technology = 8

    }
    static class ProjectCategoryUti
    {
        public static ProjectCategory GetCategory(int category)
        {
            switch (category)
            {
                case 1: return ProjectCategory.Art;
                case 2: return ProjectCategory.Comics;
                case 3: return ProjectCategory.Design;
                case 4: return ProjectCategory.Fashion;
                case 5: return ProjectCategory.Games;
                case 6: return ProjectCategory.Journalism;
                case 7: return ProjectCategory.Music;
                case 8: return ProjectCategory.Technology;

                default: return ProjectCategory.Invalid;
            }
        }
    }
}