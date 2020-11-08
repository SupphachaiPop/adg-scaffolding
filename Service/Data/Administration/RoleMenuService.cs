using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO.Backend;
using Definitions;
using Entity.Backend;

namespace Service.Backend
{
    public class RoleMenuService : IServiceRepository<RoleMenuEntity>
    {
        RoleMenuDAO RoleMenuDAO = new RoleMenuDAO();

        public List<RoleMenuEntity> GetDataAll()
        {
            return RoleMenuDAO.GetDataAll();
        }

        public RoleMenuEntity GetDataByID(long id)
        {
            return RoleMenuDAO.GetDataByID(id);
        }


        public List<RoleMenuEntity> GetDataByCondition(RoleMenuEntity entity)
        {
            throw new NotImplementedException();
        }
        public List<RoleMenuEntity> GetDataByCondition(RoleMenuEntity entity, int index)
        {
            throw new NotImplementedException();
        }

        public List<RoleMenuEntity> GetDataByCondition(int role_id, int comapny_id)
        {
            return RoleMenuDAO.GetDataByCondition(role_id, comapny_id);
        }
        public List<RoleMenuMasterEntity> GetMenuByRole(int role_id)
        {
            List<RoleMenuMasterEntity> menuMasterEntities = new List<RoleMenuMasterEntity>();
            List<RoleMenuMasterEntity> resMenu = new List<RoleMenuMasterEntity>();

            menuMasterEntities = RoleMenuDAO.GetMenuByRole(role_id);
            menuMasterEntities = menuMasterEntities.Where(x => x.is_display).ToList();
            menuMasterEntities.ForEach(item =>
            {
                item.url = string.IsNullOrEmpty(item.url) ? StaticUrl.DEFAULT_EMPTY_URL : StaticUrl.BASE_URL + item.url;
            });

            var parentMenu = menuMasterEntities.GroupBy(x => x.parent_menu_id,
                (key, group) => new { parent_menu_id = key, parent_menu = group.ToList() }).ToList();

            resMenu = parentMenu.Select(x => x.parent_menu.FirstOrDefault()).ToList();
            resMenu.ForEach(item =>
            {
                var childMenu = menuMasterEntities.Where(x => item.parent_menu_id == x.parent_menu_id).ToList();
                item.child_menu.AddRange(childMenu);
            });

            return resMenu;
        }

        public int InsertData(RoleMenuEntity entity)
        {
            return RoleMenuDAO.InsertData(entity);
        }

        public int UpdateData(RoleMenuEntity entity)
        {
            return RoleMenuDAO.UpdateData(entity);
        }

        public int UpdateDataStatus(RoleMenuEntity entity)
        {
            return RoleMenuDAO.UpdateDataStatus(entity);
        }

        public int DeleteData(RoleMenuEntity entity)
        {
            return RoleMenuDAO.DeleteData(entity);
        }

        public bool CheckIsReferred(RoleMenuEntity RoleMenuEntity)
        {
            return RoleMenuDAO.CheckIsReferred(RoleMenuEntity);
        }
    }
}
