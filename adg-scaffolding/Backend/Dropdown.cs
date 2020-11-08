using Entity.Backend;
using Service.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace adg_scaffolding.Backend
{
    public class Dropdown
    {
        #region Company
        public DropDownList GetDropdownCompany(DropDownList dropdown)
        {
            CompanyService companyService = new CompanyService();
            var companyList = companyService.GetDataAll();
            if (companyList.Count() > 0)
            {
                companyList = companyList.Where(i => i.is_active == true).ToList();
                dropdown.DataSource = companyList;
                companyList.Insert(0, new CompanyEntity
                {
                    company_id = 0,
                    company_name = "-- Select --"
                });

                dropdown.DataValueField = "company_id";
                dropdown.DataTextField = "company_name";
                dropdown.DataBind();
            }

            return dropdown;

        }
        #endregion

        #region Company Type
        public DropDownList GetDropdownCompanyType(DropDownList dropdown)
        {
            CompanyTypeService companyTypeService = new CompanyTypeService();
            var companyTypeList = companyTypeService.GetDataAll();
            if (companyTypeList.Count() > 0)
            {
                companyTypeList = companyTypeList.Where(i => i.is_active == true).ToList();
                dropdown.DataSource = companyTypeList;
                companyTypeList.Insert(0, new CompanyTypeEntity
                {
                    company_type_id = 0,
                    company_type_name = "-- Select --"
                });

                dropdown.DataValueField = "company_type_id";
                dropdown.DataTextField = "company_type_name";
                dropdown.DataBind();
            }
            return dropdown;
        }
        #endregion

        #region Branch 
        public DropDownList GetDropdownBranch(DropDownList dropdown, int company_id)
        {
            swBranchService branchService = new swBranchService();
            var branch = branchService.GetDataAll();
            if (branch.Count() > 0)
            {
                branch = branch.Where(i => i.is_active == true && i.company_id == company_id).ToList();
                dropdown.DataSource = branch;
                branch.Insert(0, new swBranchEntity
                {
                    branch_id = 0,
                    branch_name = "-- Select --"
                });

                dropdown.DataValueField = "branch_id";
                dropdown.DataTextField = "branch_name";
                dropdown.DataBind();
            }

            return dropdown;
        }
        #endregion

        #region Branch Type
        public DropDownList GetDropdownBranchType(DropDownList dropdown)
        {
            swBranchTypeService branchTypeService = new swBranchTypeService();
            var branchTypeList = branchTypeService.GetDataAll();
            if (branchTypeList.Count() > 0)
            {
                branchTypeList = branchTypeList.Where(i => i.is_active == true).ToList();
                dropdown.DataSource = branchTypeList;
                branchTypeList.Insert(0, new swBranchTypeEntity
                {
                    branch_type_id = 0,
                    branch_type_name = "-- Select --"
                });

                dropdown.DataValueField = "branch_type_id";
                dropdown.DataTextField = "branch_type_name";
                dropdown.DataBind();
            }

            return dropdown;
        }
        #endregion

        #region Role 
        public DropDownList GetDropdownRole(DropDownList dropdown)
        {
            RoleService roleService = new RoleService();
            var role = roleService.GetDataAll();
            if (role.Count() > 0)
            {
                role = role.Where(i => i.is_active == true).ToList();
                dropdown.DataSource = role;
                role.Insert(0, new RoleEntity
                {
                    role_id = 0,
                    role_name = "-- Select --"
                });

                dropdown.DataValueField = "role_id";
                dropdown.DataTextField = "role_name";
                dropdown.DataBind();
            }

            return dropdown;
        }
        #endregion
    }
}