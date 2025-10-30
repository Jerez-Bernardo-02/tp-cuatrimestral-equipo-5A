<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageRecepcionista.Master" AutoEventWireup="true" CodeBehind="RecepcionistaTurnos.aspx.cs" Inherits="Presentacion.RecepcionistaTurnos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="padding-top: 80px;">
        <div class="row mb-3">
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtEspecialidad" CssClass="form-control" Placeholder="Especialidad"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtFecha" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
            <div class="col-md-1">
                <asp:Button runat="server" Text="Filtrar" CssClass="btn btn-primary w-100" />
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <div class="card shadow-sm border-0 mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Turnos registrados</h5>

                        <asp:GridView runat="server" ID="dgvTurnos" AutoGenerateColumns="False" CssClass="table table-striped">
                            <Columns>
                                <asp:BoundField HeaderText="Fecha" />
                                <asp:BoundField HeaderText="Medico" />
                                <asp:BoundField HeaderText="Paciente" />
                                <asp:BoundField HeaderText="Especialidad" />

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
                    <asp:Button runat="server" ID="btnNuevoTurno" Text="Agregar nuevo turno" CssClass="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
