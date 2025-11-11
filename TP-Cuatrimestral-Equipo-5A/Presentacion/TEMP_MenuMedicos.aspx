<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TEMP_MenuMedicos.aspx.cs" Inherits="Presentacion.MenuMedicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="mb-4">
            <h1 class="display-5">Buenos días Dr.
                <asp:Label ID="lblNombreMedico" runat="server" Text="Label"></asp:Label>!</h1>
            <p>Resumen de tu jornada.</p>
        </div>

        <div class="row">
            <div class="col-md-8">
                <div class="card shadow-sm border-0 mb-3">

                    <%--Fila de filtros--%>
                    <div class="row mt-4 ms-3">
                        <%--Filtro Nombre--%>
                        <div class="col-md-2">
                            <label class="form-label d-block">Nombre</label>
                            <asp:TextBox ID="txtNombrePaciente" runat="server" CssClass="form-control"
                                placeholder="Nombre..."
                                AutoPostBack="true"
                                OnTextChanged="Filtro_Changed" />
                        </div>

                        <%--Filtro Apellido--%>
                        <div class="col-md-2">
                            <label class="form-label d-block">Apellido</label>
                            <asp:TextBox ID="txtApellidoPaciente" runat="server" CssClass="form-control"
                                placeholder="Apellido..."
                                AutoPostBack="true"
                                OnTextChanged="Filtro_Changed" />
                        </div>

                        <%--Filtro DNI--%>
                        <div class="col-md-2">
                            <label class="form-label d-block">DNI</label>
                            <asp:TextBox ID="txtDniPaciente" runat="server" CssClass="form-control"
                                placeholder="DNI..."
                                AutoPostBack="true"
                                OnTextChanged="Filtro_Changed" />
                        </div>

                        <%--Filtro Fecha--%>
                        <div class="col-md-3">
                            <label class="form-label d-block">Filtrar por fecha</label>
                            <asp:TextBox ID="txtFiltrarFecha" runat="server" TextMode="Date" CssClass="form-control"
                                AutoPostBack="true"
                                OnTextChanged="Filtro_Changed" />
                        </div>

                        <%--Filtro Estado--%>
                        <div class="col-md-3">
                            <label class="form-label d-block">Filtrar por estado</label>
                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="Filtro_Changed">
                            </asp:DropDownList>
                        </div>
                    </div>


                    <div class="row mt-3 ms-3 me-2">
                        <%--Grilla de turnos--%>
                        <asp:GridView ID="dgvTurnos" runat="server"
                            CssClass="table table-hover"
                            AutoGenerateColumns="false"
                            OnSelectedIndexChanged="dgvTurnos_SelectedIndexChanged"
                            ItemType="Dominio.Turno"
                            DataKeyNames="Id">
                            <Columns>
                                <asp:BoundField HeaderText="Fecha y hora" DataField="Fecha" />

                                <asp:TemplateField HeaderText="Paciente">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Item.Paciente.Nombre + " " + Item.Paciente.Apellido %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField HeaderText="Especialidad" DataField="Especialidad.Descripcion" />

                                <asp:BoundField HeaderText="Estado" DataField="Estado.Descripcion" />

                                <asp:TemplateField HeaderText="Acciones">

                                    <ItemTemplate>
                                        <asp:LinkButton runat="server"
                                            CommandName="VerHC"
                                            CommandArgument='<%# Item.Id %>'
                                            CssClass="btn btn-info btn-sm"
                                            ToolTip="Ver Historia Clínica">
                                            <i class="bi bi-eye-fill"></i>
                                        </asp:LinkButton>

                                        <asp:LinkButton runat="server"
                                            CommandName="ModificarEstado"
                                            CommandArgument='<%# Item.Id %>'
                                            CssClass="btn btn-warning btn-sm"
                                            ToolTip="Modificar Estado / Ver Observaciones">
                                            <i class="bi bi-pencil-fill"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>



                            </Columns>
                        </asp:GridView>
                    </div>

                </div>
            </div>

            <div class="col-md-4">
                <div class="card shadow-sm border-0 mb-3">
                    <div class="card-body">
                        <h5 class="card-title fs-5">Resumen del día </h5>
                        <hr />
                        <p class="card-text">Pacientes atendidos: 2/10</p>
                        <p class="card-text">Resultados pendientes: 3</p>
                        <p class="card-text">...</p>
                        <p class="card-text">...</p>
                        <p class="card-text">...</p>

                    </div>
                </div>
            </div>

        </div>

    </div>
</asp:Content>
