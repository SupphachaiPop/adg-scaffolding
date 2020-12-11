<%@ Page Title="" Language="C#" MasterPageFile="~/Backend/MasterPage.Master" AutoEventWireup="true" CodeBehind="job-cleaning-info.aspx.cs" Inherits="adg_scaffolding.Backend.Job_Management.Cleaning.job_cleaning_info" %>

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

                <h4 class="font-weight-bold py-3 mb-2">JOB CLEANING
                            <div class="text-muted text-tiny mt-1">
                                <small class="font-weight-normal text-uppercase">
                                    <a href="javascript:void(0)" class="mr-1">ADG SCAFFOLDING</a>/
                                    JOB CLEANING
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
                                                Location Name
                                                <span class="text-danger">*</span>
                                            </label>
                                            <asp:TextBox ID="txtLocationName" CssClass="form-control form-control-md " ReadOnly="true" Enabled ="false" runat="server" placeholder="Location Name"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-xl-4 mb-4 col-sm-12">
                                        <div class="form-group">
                                            <label class="form-label form-label-sm text-uppercase">
                                                Zone Name
                                                <span class="text-danger">*</span>
                                            </label>
                                            <asp:TextBox ID="txtZoneName" CssClass="form-control form-control-md" ReadOnly="true" Enabled ="false" runat="server" placeholder="Zone Name"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <hr class="mb-3">
                                <h4>Product Job</h4>
                                <hr class="mb-3">
                                <div class="row">
                                    <div class="col-lg-4 col-xl-4 mb-4 col-sm-12">
                                        <div class="form-group">
                                            <label class="form-label form-label-sm text-uppercase">
                                                Select Product
                                                <span class="text-danger">*</span>
                                            </label>
                                            <asp:DropDownList ID="ddlProduct" CssClass="form-control form-control-sm text-uppercase  js-example-basic-single" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-xl-4 mb-4 col-sm-12">
                                        <div class="form-group">
                                            <label class="form-label form-label-sm text-uppercase">
                                                Amount
                                                <span class="text-danger">*</span>
                                            </label>
                                            <asp:TextBox ID="txtAmount" CssClass="form-control form-control-md" runat="server" placeholder="Amount" TextMode="Number"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-xl-4 mb-4 col-sm-12">
                                        <div class="form-group">
                                            <label class="form-label form-label-sm text-uppercase">
                                            <asp:LinkButton ID="btnCreateJobCleaning" CssClass="btn btn-success mt-4" OnClick="btnCreateJobCleaning_Click" runat="server"> <span class="ion ion-md-add mr-1"></span> ADD</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-datatable table-responsive">
                                    <table class="datatables-demo table table-striped table-hover table-bordered">
                                        <thead class="thead-dark">
                                            <tr>
                                                <th class="tbw-2">No.</th>
                                                <th class="tbw-10">Product Name</th>
                                                <th class="tbw-10">Amount</th>
                                                <th class="tbw-10">Comment</th>
                                                <th class="tbw-2">Tools</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptJobCleaning" runat="server" OnItemDataBound="rptJobCleaning_ItemDataBound" OnItemCommand="rptJobCleaning_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr class="odd gradeX">
                                                        <td>
                                                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblJobId" Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblProductId" Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblProductName" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtComment" CssClass="form-control form-control-md" runat="server" placeholder="Comment"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lbnDelete" runat="server" CssClass="btn btn-danger" ClientIDMode="AutoID">
                                                        <i class="ion ion-md-close"></i>
                                                            </asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>

                                </div>

                                <hr class="my-3">
                                <asp:LinkButton ID="lbnBack" runat="server" CssClass="btn btn-lg btn-secondary" OnClick="lbnBack_Click">Back</asp:LinkButton>
                                <asp:LinkButton ID="lbnSave" runat="server" CssClass="btn btn-lg btn-primary" OnClick="lbnSave_Click" OnClientClick="clearFilter();">Save</asp:LinkButton>
                            </div>
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
