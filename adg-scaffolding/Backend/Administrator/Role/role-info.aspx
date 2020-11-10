<%@ Page Title="" Language="C#" MasterPageFile="~/Backend/MasterPage.Master" AutoEventWireup="true" CodeBehind="role-info.aspx.cs" Inherits="adg_scaffolding.Backend.Administrator.Role.role_info" %>

<%@ MasterType VirtualPath="~/Backend/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FromPlaceHolder" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script>
        function clearFilter() {
            $('#inbound-info').dataTable({
                paging: false,
                destroy: true,
                deferRender: true,
            });
            $('#inbound-info').dataTable().fnFilter('');
        }
    </script>

    <!-- Content -->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid flex-grow-1 container-p-y">

                <h4 class="font-weight-bold py-3 mb-2">ROLE
                            <div class="text-muted text-tiny mt-1">
                                <small class="font-weight-normal text-uppercase">
                                    <a href="javascript:void(0)" class="mr-1">XCITE COVERAGE</a>/
                                    ROLE
                                </small>
                            </div>
                    <br />
                </h4>

                <!-- Statistics -->
                <div class="card mb-3">
                    <!-- <h6 class="card-header with-elements">
                                <div class="card-header-title">Filters by</div>
                            </h6> -->
                    <div class="card-body">
                        <div class="row no-gutters row-bordered">
                            <div class="col-md-12 col-lg-12 col-xl-12">
                                <ul class="nav nav-tabs" id="myTab" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" id="info-tab" data-toggle="tab" href="#info" role="tab" aria-controls="info" aria-selected="true">Info</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" id="feature-tab" data-toggle="tab" href="#feature" role="tab" aria-controls="feature" aria-selected="false">Feature</a>
                                    </li>
                                </ul>

                                <div class="tab-content" id="myTabContent">
                                    <div class="tab-pane fade show active" id="info" role="tabpanel" aria-labelledby="info-tab">
                                        <hr class="mb-3">
                                        <div class="row">
                                            <div class="col-lg-4 col-xl-4 mb-4 col-sm-12">
                                                <div class="form-group">
                                                    <label class="form-label form-label-sm text-uppercase">
                                                        Role Code 
                                                <span class="text-danger">*</span>
                                                    </label>
                                                    <asp:Label ID="lblRoleId" runat="server" Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txtRoleCode" CssClass="form-control form-control-md" runat="server" placeholder="Role Code"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-xl-4 mb-4 col-sm-12">
                                                <div class="form-group">
                                                    <label class="form-label form-label-sm text-uppercase">
                                                        Role Name
                                                <span class="text-danger">*</span>
                                                    </label>
                                                    <asp:TextBox ID="txtRoleName" CssClass="form-control form-control-md" runat="server" placeholder="Role Name"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-xl-12 mb-12 col-sm-12">
                                                <div class="form-group">
                                                    <label class="form-label form-label-sm text-uppercase">Comment</label>
                                                    <asp:TextBox ID="txtComment" CssClass="form-control form-control-md" TextMode="MultiLine" Rows="3" runat="server" placeholder="Comment"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12 col-xl-12 mb-12 col-sm-12">
                                                <div class="form-group">
                                                    <label class="form-label form-label-sm text-uppercase">Status</label>
                                                    <label class="switcher switcher-md">
                                                        <input id="chkStatus" type="checkbox" class="switcher-input" runat="server">
                                                        <span class="switcher-indicator">
                                                            <span class="switcher-yes">
                                                                <span class="ion ion-md-checkmark"></span>
                                                            </span>
                                                            <span class="switcher-no">
                                                                <span class="ion ion-md-close"></span>
                                                            </span>
                                                        </span>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="feature" role="tabpanel" aria-labelledby="feature-tab">
                                        <hr class="mb-3">
                                        <div class="card-datatable table-responsive">
                                            <table class="datatables-demo table table-striped table-hover table-bordered">
                                                <thead class="thead-dark">
                                                    <tr>
                                                        <th class="tbw-10">Menu Name</th>
                                                        <th class="tbw-2">Display</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rptRoleMenu" runat="server" OnItemDataBound="rptRoleMenu_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr class="odd gradeX">
                                                                <td>
                                                                    <asp:Label ID="lblRoleMenuId" Visible="false" runat="server"></asp:Label>
                                                                    <asp:Label ID="lblMenuId" Visible="false" runat="server"></asp:Label>
                                                                    <asp:Label ID="lblMenuName" runat="server"></asp:Label>
                                                                </td>
                                                                <td class="center">
                                                                    <label class="switcher switcher-sm">
                                                                        <asp:HiddenField ID="hdfStatus" runat="server" />

                                                                        <input id="chkStatus" runat="server" type="checkbox" class="switcher-input">
                                                                        <span class="switcher-indicator">
                                                                            <span class="switcher-yes">
                                                                                <span class="ion ion-md-checkmark"></span>
                                                                            </span>
                                                                            <span class="switcher-no">
                                                                                <span class="ion ion-md-close"></span>
                                                                            </span>
                                                                        </span>

                                                                    </label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>

                                        </div>
                                    </div>
                                </div>
                                <hr class="my-3">
                                <asp:LinkButton ID="lbnBack" runat="server" CssClass="btn btn-lg btn-secondary" OnClick="lbnBack_Click">Back</asp:LinkButton>
                                <asp:LinkButton ID="lbnSave" runat="server" CssClass="btn btn-lg btn-primary" OnClick="lbnSave_Click" OnClientClick="clearFilter();">Save</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- / Statistics -->
            </div>
            <!-- / Content -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ModalPlaceHolder" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type='text/javascript'>
        function openModalError(msg) {
            document.getElementById("pError").innerHTML = msg;
            $('#ModalError').modal('show');
        }
        function openModalWaring(msg) {
            document.getElementById("pWaring").innerHTML = msg;
            $('#ModalWarning').modal('show');
        }
        function openModalSuccess(msg) {
            document.getElementById("pSuccess").innerHTML = msg;
            $('#ModalSuccess').modal('show');
        }
    </script>

    <div class="modal fade" id="ModalSuccess" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h3 class="modal-title text-success text-center mb-0">Success                         
                    </h3>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">×</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <p id="pSuccess"></p>
                        </div>
                    </div>
                </div>
                <hr class="m-0">
                <div class="modal-footer">
                    <asp:LinkButton ID="lblSuccess" runat="server" CssClass="btn btn-default" OnClick="lblSuccess_Click">OK</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="ModalWarning" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h3 class="modal-title text-warning text-center mb-0">Warning                        
                    </h3>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">×</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <p id="pWaring"></p>
                        </div>
                    </div>
                </div>
                <hr class="m-0">
                <div class="modal-footer">
                    <a href="#" class="btn btn-default" data-dismiss="modal">Cancel</a>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="ModalError" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h3 class="modal-title text-danger text-center mb-0">ERROR                        
                    </h3>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">×</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <p id="pError"></p>
                        </div>
                    </div>
                </div>
                <hr class="m-0">
                <div class="modal-footer">
                    <a href="#" class="btn btn-default" data-dismiss="modal">Cancel</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
