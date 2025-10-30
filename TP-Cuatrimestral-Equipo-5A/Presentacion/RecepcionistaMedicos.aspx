<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageRecepcionista.Master" AutoEventWireup="true" CodeBehind="RecepcionistaMedicos.aspx.cs" Inherits="Presentacion.RecepcionistaMedicos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="padding-top: 80px;">
        <div class="row mb-3">
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtMatricula" CssClass="form-control" Placeholder="N° Matricula"></asp:TextBox>
            </div>
            <div class="col-md-1">
                <asp:Button runat="server" Text="Filtrar" CssClass="btn btn-primary w-100" />
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <div class="card shadow-sm border-0 mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Medicos registrados</h5>

                        <asp:GridView runat="server" ID="dgvMedicos" AutoGenerateColumns="False" CssClass="table table-striped">
                            <Columns>
                                <asp:BoundField HeaderText="N° Matricula" />
                                <asp:BoundField HeaderText="Nombre" />
                                <asp:BoundField HeaderText="Apellido" />
                                <asp:BoundField HeaderText="Email" />
                                <asp:BoundField HeaderText="Telefono" />

                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:Button runat="server" Text="Editar" CssClass="btn btn-sm btn-primary" />
                                        <asp:Button runat="server" Text="Turnos" CssClass="btn btn-sm btn-secondary" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="text-end">
                    <asp:Button runat="server" ID="btnNuevoTurno" Text="Nuevo Médico" CssClass="btn btn-success" />
                </div>
            </div>
        </div>
</div>
</asp:Content>
