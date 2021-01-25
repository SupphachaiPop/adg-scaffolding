<%@ Page Language="C#" MasterPageFile="~/Backend/MasterPage.Master" AutoEventWireup="true" CodeBehind="job-list.aspx.cs" Inherits="adg_scaffolding.Backend.Job_Management.Job.job_list" %>

<%@ MasterType VirtualPath="~/Backend/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FromPlaceHolder" runat="server">
    <asp:UpdatePanel ID="upnInboud" runat="server">
        <ContentTemplate>
            <div class="container-fluid flex-grow-1 container-p-y">

                <h4 class="font-weight-bold py-3 mb-2">JOB CLEANING
                            <div class="text-muted text-tiny mt-1">
                                <small class="font-weight-normal text-uppercase">
                                    <a href="javascript:void(0)" class="mr-1">ADG SCAFFOLDING</a>/
                                    JOB
                                </small>
                            </div>
                </h4>

                <!-- Statistics -->
                <asp:HiddenField ID="hdfId" runat="server" />

                <div class="card mb-3">
                    <div class="row no-gutters row-bordered">
                        <div class="col-md-12 col-lg-12 col-xl-12">
                            <div class="card-body">
                                <div class="row">   
                                    <div class="col-lg-6 col-xl-6 mb-12">
                                        <div class="form-group">
                                            <label class="form-label form-label-sm text-uppercase">Search</label>
                                            <asp:TextBox ID="txtSearch" CssClass="form-control form-control-md" runat="server" placeholder="ค้นหา"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-xl-3 mb-12">
                                        <div class="form-group">
                                            <label class="form-label form-label-sm text-uppercase">Warehouse</label>
                                            <asp:DropDownList ID="ddlWarehouse" CssClass="form-control form-control-sm text-uppercase js-example-basic-single" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-xl-3 mb-12">
                                        <div class="form-group">
                                            <label class="form-label form-label-sm text-uppercase">Location</label>
                                            <asp:DropDownList ID="ddlLocation" CssClass="form-control form-control-sm text-uppercase js-example-basic-single" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <button type="button" id="btnSearch" class="btn btn-primary mt-4">SEARCH</button>
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
                                    <table id="tblJob" class="table table-hover table-bordered">
                                        <thead class="thead-dark">
                                            <tr>
                                                <th>Warehouse Name</th>
                                                <th>Location Name</th>
                                                <th>Zone Name</th>
                                                <th>Amount</th>
                                                <th>Comment</th>
                                                <th>Tools</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Label ID="lblId" runat="server"></asp:Label>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <!-- / Sale stats -->
                        </div>
                    </div>
                </div>
            </div>
            <!-- / Content -->
        </ContentTemplate>
    </asp:UpdatePanel>

    <script src="/assets/js/JobManagement/job-list.js"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ModalPlaceHolder" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type='text/javascript'>
        function openModalDeleteRecord() {
            $('#DeleteRecord').modal('show');
        }
        function openModalError(msg) {
            document.getElementById("pError").innerHTML = msg;
            $('#ModalError').modal('show');
        }

        function openModalFail(msg) {
            document.getElementById("pFail").innerHTML = msg;
            $('#ModalFail').modal('show');
        }

        function openModalWaring(msg) {
            document.getElementById("pWaring").innerHTML = msg;
            $('#ModalWaring').modal('show');
        }
        function openModalSuccess(msg) {
            document.getElementById("pSuccess").innerHTML = msg;
            $('#ModalSuccess').modal('show');
        }
    </script>

    <div class="modal fade" id="DeleteRecord" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h3 class="modal-title text-danger text-center mb-0">Are you sure?                        
                    </h3>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">×</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            ต้องการลบรายการนี้หรือไม่ ?
                        </div>
                    </div>
                </div>
                <hr class="m-0">
                <div class="modal-footer">
                    <a href="#" class="btn btn-default" data-dismiss="modal">Cancel</a>
                    <button id="btnconfirm" type="button" class="btn btn-danger" style="display: inline-block;" onclick="DeleteData();">Confirm</button>

                </div>
            </div>
        </div>
    </div>

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
                    <a href="#" class="btn btn-default" data-dismiss="modal">OK</a>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="ModalFail" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h3 class="modal-title text-danger text-center mb-0">Failed                       
                    </h3>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">×</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <p id="pFail"></p>
                        </div>
                    </div>
                </div>
                <hr class="m-0">
                <div class="modal-footer">
                    <a href="#" class="btn btn-default" data-dismiss="modal">OK</a>
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
