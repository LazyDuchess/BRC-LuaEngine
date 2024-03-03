using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaEngine
{
    public static class LuaReloadableManager
    {
        private static List<WeakReference> Reloadables = [];

        public static void Register(ILuaReloadable reloadable)
        {
            Purge();
            Reloadables.Add(new WeakReference(reloadable));
        }

        internal static void Purge()
        {
            Reloadables = Reloadables.Where(reloadable => reloadable.IsAlive).ToList();
        }

        internal static void OnReload()
        {
            Purge();
            foreach(ILuaReloadable reloadable in Reloadables)
            {
                reloadable.OnReload();
            }
        }
    }
}
