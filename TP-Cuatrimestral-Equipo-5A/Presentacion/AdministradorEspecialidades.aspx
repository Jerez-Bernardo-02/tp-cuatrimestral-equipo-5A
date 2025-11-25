<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdministradorEspecialidades.aspx.cs" Inherits="Presentacion.AdministradorEspecialidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3 class="fw-bold mb-4">Gestión Especialidades</h3>

    <%--Menu--%>
    <ul class="nav nav-tabs mt-3">

        <li class="nav-item">
            <asp:LinkButton Text="Administrar Especialidades" runat="server" CssClass="nav-link active" ID="lnkBtnAdministrarEspecialidades" OnClick="lnkBtnAdministrarEspecialidades_Click" />
        </li>

        <li class="nav-item">
            <asp:LinkButton Text="Asignar a Médicos" runat="server" CssClass="nav-link" ID="lnkBtnGestionarEspMedicos" OnClick="lnkBtnGestionarEspMedicos_Click" />
        </li>

    </ul>

    <%--Panel de ABM especialidades--%>
    <asp:Panel runat="server" ID="pnlAdministrarEspecialidades">

        <div class="col-md-8">
            <div class="card shadow-sm">
                <div class="card-body">

                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="dgvEspecialidades" DataKeyNames="Id" AutoGenerateColumns="False" CssClass="table table-striped align-middle" AllowPaging="true" PageSize="10" OnSelectedIndexChanged="dgvEspecialidades_SelectedIndexChanged" OnPageIndexChanging="dgvEspecialidades_PageIndexChanging">

                            <Columns>

                                <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />

                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    <HeaderStyle HorizontalAlign="Center" />

                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkBtnModificarDatos"
                                            CssClass="btn btn-warning btn-sm d-flex align-items-center justify-content-center gap-1"
                                            CommandName="Select" ToolTip="Modificar datos">

                                    <i class="bi bi-pencil-fill"></i> 
                                    Editar
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>
                    </div>

                </div>
            </div>
        </div>

        <div class="mt-4 d-flex justify-content-between">
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" CssClass="btn btn-primary" />
        </div>
    </asp:Panel>


    <%--Panel de asignación a médicos--%>
    <asp:Panel runat="server" ID="pnlGestionarEspMedicos">
            <%--Mensaje Exitoso o Error--%>
    <div class="row">
        <div class="col-12">
            <%-- Label de ERROR (Rojo) --%>
            <asp:Label ID="lblMensajeError" runat="server"
                CssClass="alert alert-danger d-block text-center" Visible="false">
            </asp:Label>

            <%-- Label de ÉXITO (Verde) --%>
            <asp:Label ID="lblMensajeExito" runat="server"
                CssClass="alert alert-success d-block text-center" Visible="false">
            </asp:Label>
        </div>
    </div>

        <div class="row">
            <%-- COLUMNA IZQUIERDA: Lista de medicos --%>
            <div class="col-md-6">
                <asp:GridView runat="server" ID="dgvMedicos" CssClass="table table-striped align-middle"
                    DataKeyNames="Id"
                    AutoGenerateColumns="False"
                    AllowPaging="true" PageSize="10"
                    OnPageIndexChanging="dgvMedicos_PageIndexChanging"
                    OnSelectedIndexChanged="dgvMedicos_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField HeaderText="Matricula" DataField="Matricula" />
                        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                        <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
                        <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" ControlStyle-CssClass="btn btn-sm btn-outline-primary" />
                    </Columns>

                </asp:GridView>
            </div>
            <%-- COLUMNA DERECHA:(Solo visible al seleccionar) --%>
            <div class="col-md-6">
                <asp:Panel ID="pnlAccionesMedico" runat="server" Visible="false" 
                 CssClass="card p-3 bg-light">

                    <h5 class="border-bottom pb-2">Editando a:
                        <asp:Label ID="lblNombreMedico" runat="server" Text="-" /></h5>

                    <%-- Agregar nueva especialidad --%>
                    <div class="mb-3 mt-3">
                        <label>Agregar nueva especialidad:</label>
                        <div class="d-flex gap-2"> <%--(Flex para poner el boton agregar al lado del dropdown)--%>
                            <asp:DropDownList ID="ddlEspecialidades" runat="server" CssClass="form-select"></asp:DropDownList>
                            <asp:Button ID="btnAgregarNuevaEsp" runat="server" Text="Agregar" CssClass="btn btn-success" OnClick="btnAgregarNuevaEsp_Click" />
                        </div>
                    </div>

                    <%-- Lista de especialidades que tiene actualmente el medico seleccionado --%>
                    <h6>Especialidades actuales:</h6>
                    <asp:GridView ID="dgvEspecialidadesDelMedico" runat="server" CssClass="table table-sm table-light"
                        AutoGenerateColumns="false" DataKeyNames="Id"
                        OnSelectedIndexChanged="dgvEspecialidadesDelMedico_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField HeaderText="Nombre especialidad" DataField="Descripcion" />
                            <asp:CommandField ShowSelectButton="true" SelectText="Quitar" ControlStyle-CssClass="btn btn-danger btn-sm" />
                        </Columns>
                    </asp:GridView>

                </asp:Panel>
            </div>

        </div>
    </asp:Panel>



</asp:Content>
