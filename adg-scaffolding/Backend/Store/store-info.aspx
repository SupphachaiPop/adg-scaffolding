<%@ Page Title="" Language="C#" MasterPageFile="~/Backend/MasterPage.Master" AutoEventWireup="true" CodeBehind="store-info.aspx.cs" Inherits="adg_scaffolding.Backend.Store.store_info" %>

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

                <h4 class="font-weight-bold py-3 mb-2">STORE
                            <div class="text-muted text-tiny mt-1">
                                <small class="font-weight-normal text-uppercase">
                                    <a href="javascript:void(0)" class="mr-1">ADG SCAFFOLDING</a>/
                                    STORE
                                </small>
                            </div>
                    <br />
                </h4>

                <!-- Statistics -->
                <div class="card mb-3">
                    <!-- <h6 class="card-header with-elements">
                                <div class="card-header-title">Filters by</div>
                            </h6> -->
                    <div class="row no-gutters row-bordered">
                        <div class="col-md-12 col-lg-12 col-xl-12">
                            <div class="card-body">
                                <hr class="mb-3">
                                <div class="row">
                                    <div class="col-lg-4 col-xl-4 mb-4 col-sm-12">
                                        <div class="form-group">
                                            <label class="form-label form-label-sm text-uppercase">
                                                Store Code
                                                <span class="text-danger">*</span>
                                            </label>
                                            <asp:Label ID="lblStoreId" runat="server" Visible="false"></asp:Label>
                                            <asp:DropDownList ID="ddlProductStore" CssClass="form-control form-control-sm text-uppercase js-example-basic-single" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-xl-4 mb-4 col-sm-12">
                                        <div class="form-group">
                                            <label class="form-label form-label-sm text-uppercase">
                                                Store Name
                                                <span class="text-danger">*</span>
                                            </label>
                                            <asp:TextBox ID="txtQty" CssClass="form-control form-control-md" TextMode="Number" runat="server" placeholder="Qty"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12 col-xl-12 mb-12 col-sm-12">
                                        <div class="form-group">
                                            <label class="form-label form-label-sm text-uppercase">Comment</label>
                                            <asp:TextBox ID="txtComment" CssClass="form-control form-control-md" TextMode="MultiLine" Rows="3" runat="server" placeholder="Comment"></asp:TextBox>
                                        </div>
                                    </div>
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
                                <hr class="my-3">
                                <asp:LinkButton ID="lbnBack" runat="server" CssClass="btn btn-lg btn-secondary" OnClick="lbnBack_Click">Back</asp:LinkButton>
                                <asp:LinkButton ID="lbnSave" runat="server" CssClass="btn btn-lg btn-primary" OnClick="lbnSave_Click" OnClientClick="clearFilter();">Save</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card mb-3">
                    <div class="row no-gutters row-bordered">
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <!-- Sale stats -->
                            <div class="card-body">
                                <div class="card-datatable table-responsive">
                                    <table id="tblStoreHistory" class="datatables-demo table table-striped table-hover table-bordered">
                                        <thead class="thead-dark">
                                            <tr>
                                                <th>Date</th>
                                                <th>location</th>
                                                <th>zone</th>
                                                <th>Qty</th>
                                                <th>Comment</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptStoreHistory" runat="server" OnItemDataBound="rptStoreHistory_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr class="odd gradeX">
                                                        <td>
                                                            <asp:Label ID="lblDate" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblLocationName" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblZoneName" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblQty" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblComment" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>

                                </div>
                            </div>
                            <!-- / Sale stats -->
                        </div>
                    </div>
                </div>
            </div>
            <!-- / Statistics -->
            </div>
            <!-- / Content -->
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="/assets/js/store/store-info.js"></script>
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
