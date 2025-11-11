<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RecepcionistaPacientes.aspx.cs" Inherits="Presentacion.RecepcionistaPacientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="padding-top: 40px;">
        <h2 class="card-title mb-5">Pacientes registrados</h2>

        <div class="row mb-3">
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtDni" CssClass="form-control" Placeholder="DNI"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" Placeholder="Nombre"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" Placeholder="Apellido"></asp:TextBox>
            </div>
            <div class="col-md-1">
                <asp:Button runat="server" Text="Filtrar" CssClass="btn btn-primary w-100" />
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <div class="card shadow-sm border-0 mb-3">
                    <div class="card-body">
                        
                        <asp:GridView runat="server" ID="dgvPacientes" AutoGenerateColumns="False" CssClass="table table-striped">
                            <Columns>
                                <asp:BoundField HeaderText="Dni" Datafield="Dni"/>
                                <asp:BoundField HeaderText="Nombre" Datafield="Nombre"/>
                                <asp:BoundField HeaderText="Apellido" Datafield="Apellido"/>
                                <asp:BoundField HeaderText="Email" Datafield="Email"/>
                                <asp:BoundField HeaderText="Telefono" Datafield="Telefono"/>

                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkBtnModificarDatos" Text="Editar" CssClass="btn btn-warning btn-sm" ToolTip="Modificar datos">
                                            <i class="bi bi-pencil-fill"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lnkBtnVerTurnos"  Text="Turnos" CssClass="btn btn-sm btn-secondary btn-sm" ToolTip="Ver Turnos Asociados">
                                            <i class="bi bi-eye-fill"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="text-end">
                    <asp:Button runat="server" ID="btnNuevoPaciente" Text="Nuevo Paciente" CssClass="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
