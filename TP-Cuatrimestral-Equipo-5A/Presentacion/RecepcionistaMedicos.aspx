<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RecepcionistaMedicos.aspx.cs" Inherits="Presentacion.RecepcionistaMedicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="padding-top: 40px;">

        <h2 class="mb-5">Medicos registrados</h2>

        <div class="row mb-3">

            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtMatricula" CssClass="form-control" Placeholder="N° Matricula"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" Placeholder="Nombre"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" Placeholder="Apellido"></asp:TextBox>
            </div>
            <div class="col-md-1">
                <asp:Button runat="server" ID="btnFiltrar" Text="Filtrar" CssClass="btn btn-primary w-100" OnClick="btnFiltrar_Click"/>
            </div>
            <div class="col-md text-end">
                <asp:Button runat="server" ID="btnNuevoMedico" Text="Nuevo Medico" CssClass="btn btn-success" OnClick="btnNuevoMedico_Click" />
            </div>

        </div>

        <div class="row">
            <div class="col-12">

                <asp:GridView runat="server" ID="dgvMedicos" AutoGenerateColumns="False" CssClass="table table-striped" OnSelectedIndexChanged="dgvMedicos_SelectedIndexChanged" OnPageIndexChanging="dgvMedicos_PageIndexChanging" AllowPaging="true" PageSize="10">
                    <Columns>
                        <asp:BoundField HeaderText="N° Matricula" DataField="Matricula" />
                        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                        <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
                        <asp:BoundField HeaderText="Email" DataField="Email" />
                        <asp:BoundField HeaderText="Telefono" DataField="Telefono" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkBtnModificarDatos" Text="Editar" CssClass="btn btn-warning btn-sm" ToolTip="Modificar datos">
                                            <i class="bi bi-pencil-fill"></i>
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="lnkBtnVerTurnos" Text="Turnos" CssClass="btn btn-sm btn-secondary btn-sm" ToolTip="Ver Turnos Asociados">
                                            <i class="bi bi-eye-fill"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

            </div>
        </div>

        <div>
            <asp:Button ID="btnVolver" runat="server" Text="Volver al Menu" CssClass="btn btn-primary" OnClick="btnVolver_Click" />
        </div>

    </div>
</asp:Content>
