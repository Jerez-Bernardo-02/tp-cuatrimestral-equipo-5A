<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MedicoHorarios.aspx.cs" Inherits="Presentacion.MedicoHorarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--Seleccion de medico:--%>
    <div class="col-md-8 mt-2 mb-2">
        <asp:DropDownList OnSelectedIndexChanged="ddlMedicos_SelectedIndexChanged" ID="ddlMedicos"
            runat="server" CssClass="form-select mt-2" AutoPostBack="true">
        </asp:DropDownList>
    </div>

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

        <%--Lunes--%>
        <div class="col-md-3 mt-2">
            <div class="card shadow-sm mb-3">
                <h4 class="card-title ms-3 mt-2 mb-2">Lunes</h4>

                <%--Checkbox Dia no disponible--%>
                <div class="form-check ms-3 mb-2">
                    <asp:CheckBox ID="chkNoDisponibleLunes" runat="server"
                        CssClass="form-check-input" AutoPostBack="true" />

                    <asp:Label ID="lblLunes" runat="server" Text="Marcar como no disponible"
                        CssClass="form-check-label" />
                </div>
                <%-- Tarjeta para CADA bloque --%>
                <div class="card-body">
                    <%--      REPEATER     --%>
                    <asp:Repeater runat="server"
                        ID="repHorarioLunes" OnItemDataBound="repHorarioLunes_ItemDataBound"> <%--OnItemDataBound para personalizar la pagina de acuerdo al codigo (cargar ddls)--%>
                        <ItemTemplate>
                            <%-- Botón de borrar --%>
                            <div class="text-end mt-1 mb-1">
                                <asp:LinkButton ID="btnBorrarBloqueLunes" CommandArgument='<%# Eval("Id")%>' OnCommand="btnBorrarBloque_Command"
                                    runat="server"
                                    CssClass="btn btn-danger btn-sm">
                                    <i class="bi bi-trash"></i>
                                </asp:LinkButton>
                            </div>

                            <%--Horario desde--%>
                            <label class="form-label">Desde</label>
                            <asp:TextBox ID="txtHoraEntradaLunes" runat="server"
                                CssClass="form-control" TextMode="Time"
                                Text='<%# Eval("HoraEntrada") %>' />

                            <%--Horario hasta--%>
                            <label class="form-label mt-2">Hasta</label>
                            <asp:TextBox ID="txtHoraSalidaLunes" runat="server"
                                CssClass="form-control" TextMode="Time"
                                Text='<%# Eval("HoraSalida") %>' />

                            <%--Especialidades--%>
                            <asp:DropDownList ID="ddlEspecialidadesLunes" runat="server" CssClass="form-select mt-2">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%--PARA AGREGAR UN NUEVO HORARIO:--%>
                    <%--Horario desde--%>
                    <label class="form-label">Desde</label>
                    <asp:TextBox ID="txtNuevaHoraEntradaLunes" runat="server"
                        CssClass="form-control" TextMode="Time"
                        Text="" />

                    <%--Horario hasta--%>
                    <label class="form-label mt-2">Hasta</label>
                    <asp:TextBox ID="txtNuevaHoraSalidaLunes" runat="server"
                        CssClass="form-control" TextMode="Time"
                        Text="" />

                    <%--Especialidades--%>
                    <asp:DropDownList ID="ddlEspNuevoHorarioLunes" runat="server" CssClass="form-select mt-2">
                    </asp:DropDownList>
                    <%--Boton confirmar horario--%>
                    <div class="text-center mt-2">
                        <asp:Button ID="btnAñadirHorarioLunes" runat="server" Text="Añadir Horario"
                            CssClass="btn btn-primary"
                            CommandArgument="1"
                            OnCommand="btnAñadirHorario_Command" /><%--1 = Lunes--%>
                    </div>
                </div>
            </div>
        </div>

        <%--Martes--%>
        <div class="col-md-3 mt-2">
            <div class="card shadow-sm mb-3">
                <h4 class="card-title ms-3 mt-2 mb-2">Martes</h4>

                <%--Checkbox Dia no disponible--%>
                <div class="form-check ms-3 mb-2">
                    <asp:CheckBox ID="chkNoDisponibleMartes" runat="server"
                        CssClass="form-check-input" AutoPostBack="true" />

                    <asp:Label ID="lblMartes" runat="server" Text="Marcar como no disponible"
                        CssClass="form-check-label" />
                </div>
                <%-- Tarjeta para CADA bloque --%>
                <div class="card-body">
                    <%--REPEATER--%>
                    <asp:Repeater runat="server"
                        ID="repHorarioMartes" OnItemDataBound="repHorarioMartes_ItemDataBound">
                        <ItemTemplate>
                            <%-- Botón de borrar --%>
                            <div class="text-end mt-1 mb-1">
                                <asp:LinkButton ID="btnBorrarBloqueMartes" CommandArgument='<%# Eval("Id")%>' OnCommand="btnBorrarBloque_Command"
                                    runat="server"
                                    CssClass="btn btn-danger btn-sm">
                                    <i class="bi bi-trash"></i>
                                </asp:LinkButton>

                            </div>

                            <%--Horario desde--%>
                            <label class="form-label">Desde</label>
                            <asp:TextBox ID="txtHoraEntradaMartes" runat="server"
                                CssClass="form-control" TextMode="Time"
                                Text='<%# Eval("HoraEntrada") %>' />

                            <%--Horario hasta--%>
                            <label class="form-label mt-2">Hasta</label>
                            <asp:TextBox ID="txtHoraSalidaMartes" runat="server"
                                CssClass="form-control" TextMode="Time"
                                Text='<%# Eval("HoraSalida") %>' />

                            <%--Especialidades--%>
                            <asp:DropDownList ID="ddlEspecialidadesMartes" runat="server" CssClass="form-select mt-2">
                                <asp:ListItem Text="Especialidades" Value="" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%--PARA AGREGAR UN NUEVO HORARIO:--%>
                    <%--Horario desde--%>
                    <label class="form-label">Desde</label>
                    <asp:TextBox ID="txtNuevaHoraEntradaMartes" runat="server"
                        CssClass="form-control" TextMode="Time"
                        Text="" />

                    <%--Horario hasta--%>
                    <label class="form-label mt-2">Hasta</label>
                    <asp:TextBox ID="txtNuevaHoraSalidaMartes" runat="server"
                        CssClass="form-control" TextMode="Time"
                        Text="" />

                    <%--Especialidades--%>
                    <asp:DropDownList ID="ddlEspNuevoHorarioMartes" runat="server" CssClass="form-select mt-2">
                    </asp:DropDownList>
                    <%--Boton confirmar horario--%>
                    <div class="text-center mt-2">
                        <asp:Button ID="btnAñadirHorarioMartes" runat="server" Text="Añadir Horario"
                            CssClass="btn btn-primary"
                            CommandArgument="2"
                            OnCommand="btnAñadirHorario_Command" /><%--2 = Martes--%>
                    </div>
                </div>
            </div>
        </div>

        <%--Miercoles--%>
        <div class="col-md-3 mt-2">
            <div class="card shadow-sm mb-3">
                <h4 class="card-title ms-3 mt-2 mb-2">Miercoles</h4>

                <%--Checkbox Dia no disponible--%>
                <div class="form-check ms-3 mb-2">
                    <asp:CheckBox ID="chkNoDisponibleMiercoles" runat="server"
                        CssClass="form-check-input" AutoPostBack="true" />

                    <asp:Label ID="lblMiercoles" runat="server" Text="Marcar como no disponible"
                        CssClass="form-check-label" />
                </div>
                <%-- Tarjeta para CADA bloque --%>
                <div class="card-body">
                    <%--REPEATER--%>
                    <asp:Repeater runat="server"
                        ID="repHorarioMiercoles" OnItemDataBound="repHorarioMiercoles_ItemDataBound">
                        <ItemTemplate>
                            <%-- Botón de borrar --%>
                            <div class="text-end mt-1 mb-1">
                                <asp:LinkButton ID="btnBorrarBloqueMiercoles" CommandArgument='<%# Eval("Id")%>' OnCommand="btnBorrarBloque_Command"
                                    runat="server"
                                    CssClass="btn btn-danger btn-sm">
                                    <i class="bi bi-trash"></i>
                                </asp:LinkButton>

                            </div>

                            <%--Horario desde--%>
                            <label class="form-label">Desde</label>
                            <asp:TextBox ID="txtHoraEntradaMiercoles" runat="server"
                                CssClass="form-control" TextMode="Time"
                                Text='<%# Eval("HoraEntrada") %>' />

                            <%--Horario hasta--%>
                            <label class="form-label mt-2">Hasta</label>
                            <asp:TextBox ID="txtHoraSalidaMiercoles" runat="server"
                                CssClass="form-control" TextMode="Time"
                                Text='<%# Eval("HoraSalida") %>' />

                            <%--Especialidades--%>
                            <asp:DropDownList ID="ddlEspecialidadesMiercoles" runat="server" CssClass="form-select mt-2">
                                <asp:ListItem Text="Especialidades" Value="" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:Repeater>

                    <%--PARA AGREGAR UN NUEVO HORARIO:--%>
                    <%--Horario desde--%>
                    <label class="form-label">Desde</label>
                    <asp:TextBox ID="txtNuevaHoraEntradaMiercoles" runat="server"
                        CssClass="form-control" TextMode="Time"
                        Text="" />

                    <%--Horario hasta--%>
                    <label class="form-label mt-2">Hasta</label>
                    <asp:TextBox ID="txtNuevaHoraSalidaMiercoles" runat="server"
                        CssClass="form-control" TextMode="Time"
                        Text="" />

                    <%--Especialidades--%>
                    <asp:DropDownList ID="ddlEspNuevoHorarioMiercoles" runat="server" CssClass="form-select mt-2">
                    </asp:DropDownList>
                    <%--Boton confirmar horario--%>
                    <div class="text-center mt-2">
                        <asp:Button ID="btnAñadirHorarioMiercoles" runat="server" Text="Añadir Horario"
                            CssClass="btn btn-primary"
                            CommandArgument="3"
                            OnCommand="btnAñadirHorario_Command" /><%--3 = Miercoles--%>
                    </div>
                </div>
            </div>
        </div>

        <%--Jueves--%>
        <div class="col-md-3 mt-2">
            <div class="card shadow-sm mb-3">
                <h4 class="card-title ms-3 mt-2 mb-2">Jueves</h4>

                <%--Checkbox Dia no disponible--%>
                <div class="form-check ms-3 mb-2">
                    <asp:CheckBox ID="chkNoDisponibleJueves" runat="server"
                        CssClass="form-check-input" AutoPostBack="true" />

                    <asp:Label ID="lblJueves" runat="server" Text="Marcar como no disponible"
                        CssClass="form-check-label" />
                </div>
                <%-- Tarjeta para CADA bloque --%>
                <div class="card-body">
                    <%--REPEATER--%>
                    <asp:Repeater runat="server"
                        ID="repHorarioJueves" OnItemDataBound="repHorarioJueves_ItemDataBound">
                        <ItemTemplate>

                            <%-- Botón de borrar --%>
                            <div class="text-end mt-1 mb-1">
                                <asp:LinkButton ID="btnBorrarBloqueJueves" CommandArgument='<%# Eval("Id")%>' OnCommand="btnBorrarBloque_Command"
                                    runat="server"
                                    CssClass="btn btn-danger btn-sm">
                                    <i class="bi bi-trash"></i>
                                </asp:LinkButton>

                            </div>

                            <%--Horario desde--%>
                            <label class="form-label">Desde</label>
                            <asp:TextBox ID="txtHoraEntradaJueves" runat="server"
                                CssClass="form-control" TextMode="Time"
                                Text='<%# Eval("HoraEntrada") %>' />

                            <%--Horario hasta--%>
                            <label class="form-label mt-2">Hasta</label>
                            <asp:TextBox ID="txtHoraSalidaJueves" runat="server"
                                CssClass="form-control" TextMode="Time"
                                Text='<%# Eval("HoraSalida") %>' />

                            <%--Especialidades--%>
                            <asp:DropDownList ID="ddlEspecialidadesJueves" runat="server" CssClass="form-select mt-2">
                                <asp:ListItem Text="Especialidades" Value="" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%--PARA AGREGAR UN NUEVO HORARIO:--%>
                    <%--Horario desde--%>
                    <label class="form-label">Desde</label>
                    <asp:TextBox ID="txtNuevaHoraEntradaJueves" runat="server"
                        CssClass="form-control" TextMode="Time"
                        Text="" />

                    <%--Horario hasta--%>
                    <label class="form-label mt-2">Hasta</label>
                    <asp:TextBox ID="txtNuevaHoraSalidaJueves" runat="server"
                        CssClass="form-control" TextMode="Time"
                        Text="" />

                    <%--Especialidades--%>
                    <asp:DropDownList ID="ddlEspNuevoHorarioJueves" runat="server" CssClass="form-select mt-2">
                    </asp:DropDownList>
                    <%--Boton confirmar horario--%>
                    <div class="text-center mt-2">
                        <asp:Button ID="btnAñadirHorarioJueves" runat="server" Text="Añadir Horario"
                            CssClass="btn btn-primary"
                            CommandArgument="4"
                            OnCommand="btnAñadirHorario_Command" /><%--4 = Jueves--%>
                    </div>

                </div>
            </div>
        </div>

        <%--Viernes--%>
        <div class="col-md-3 mt-2">
            <div class="card shadow-sm mb-3">
                <h4 class="card-title ms-3 mt-2 mb-2">Viernes</h4>

                <%--Checkbox Dia no disponible--%>
                <div class="form-check ms-3 mb-2">
                    <asp:CheckBox ID="chkNoDisponibleViernes" runat="server"
                        CssClass="form-check-input" AutoPostBack="true" />

                    <asp:Label ID="lblViernes" runat="server" Text="Marcar como no disponible"
                        CssClass="form-check-label" />
                </div>
                <%-- Tarjeta para CADA bloque --%>
                <div class="card-body">
                    <%--REPEATER--%>
                    <asp:Repeater runat="server"
                        ID="repHorarioViernes" OnItemDataBound="repHorarioViernes_ItemDataBound">
                        <ItemTemplate>

                            <%-- Botón de borrar --%>
                            <div class="text-end mt-1 mb-1">
                                <asp:LinkButton ID="btnBorrarBloqueViernes" CommandArgument='<%# Eval("Id")%>' OnCommand="btnBorrarBloque_Command"
                                    runat="server"
                                    CssClass="btn btn-danger btn-sm">
                                    <i class="bi bi-trash"></i>
                                </asp:LinkButton>

                            </div>

                            <%--Horario desde--%>
                            <label class="form-label">Desde</label>
                            <asp:TextBox ID="txtHoraEntradaViernes" runat="server"
                                CssClass="form-control" TextMode="Time"
                                Text='<%# Eval("HoraEntrada") %>' />

                            <%--Horario hasta--%>
                            <label class="form-label mt-2">Hasta</label>
                            <asp:TextBox ID="txtHoraSalidaViernes" runat="server"
                                CssClass="form-control" TextMode="Time"
                                Text='<%# Eval("HoraSalida") %>' />

                            <%--Especialidades--%>
                            <asp:DropDownList ID="ddlEspecialidadesViernes" runat="server" CssClass="form-select mt-2">
                                <asp:ListItem Text="Especialidades" Value="" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%--PARA AGREGAR UN NUEVO HORARIO:--%>

                    <%--Horario desde--%>
                    <label class="form-label">Desde</label>
                    <asp:TextBox ID="txtNuevaHoraEntradaViernes" runat="server"
                        CssClass="form-control" TextMode="Time"
                        Text="" />

                    <%--Horario hasta--%>
                    <label class="form-label mt-2">Hasta</label>
                    <asp:TextBox ID="txtNuevaHoraSalidaViernes" runat="server"
                        CssClass="form-control" TextMode="Time"
                        Text="" />

                    <%--Especialidades--%>
                    <asp:DropDownList ID="ddlEspNuevoHorarioViernes" runat="server" CssClass="form-select mt-2">
                    </asp:DropDownList>
                    <%--Boton confirmar horario--%>
                    <div class="text-center mt-2">
                        <asp:Button ID="btnAñadirHorarioViernes" runat="server" Text="Añadir Horario"
                            CssClass="btn btn-primary"
                            CommandArgument="5"
                            OnCommand="btnAñadirHorario_Command" /><%--5 = Viernes--%>
                    </div>
                </div>
            </div>
        </div>

        <%--Sabado--%>
        <div class="col-md-3 mt-2">
            <div class="card shadow-sm mb-3">
                <h4 class="card-title ms-3 mt-2 mb-2">Sabado</h4>

                <%--Checkbox Dia no disponible--%>
                <div class="form-check ms-3 mb-2">
                    <asp:CheckBox ID="chkNoDisponibleSabado" runat="server"
                        CssClass="form-check-input" AutoPostBack="true" />

                    <asp:Label ID="lblSabado" runat="server" Text="Marcar como no disponible"
                        CssClass="form-check-label" />
                </div>
                <%-- Tarjeta para CADA bloque --%>
                <div class="card-body">
                    <%--REPEATER--%>
                    <asp:Repeater runat="server"
                        ID="repHorarioSabado" OnItemDataBound="repHorarioSabado_ItemDataBound">
                        <ItemTemplate>

                            <%-- Botón de borrar --%>
                            <div class="text-end mt-1 mb-1">
                                <asp:LinkButton ID="btnBorrarBloqueSabado" CommandArgument='<%# Eval("Id")%>' OnCommand="btnBorrarBloque_Command"
                                    runat="server"
                                    CssClass="btn btn-danger btn-sm">
                                    <i class="bi bi-trash"></i>
                                </asp:LinkButton>

                            </div>

                            <%--Horario desde--%>
                            <label class="form-label">Desde</label>
                            <asp:TextBox ID="txtHoraEntradaSabado" runat="server"
                                CssClass="form-control" TextMode="Time"
                                Text='<%# Eval("HoraEntrada") %>' />

                            <%--Horario hasta--%>
                            <label class="form-label mt-2">Hasta</label>
                            <asp:TextBox ID="txtHoraSalidaSabado" runat="server"
                                CssClass="form-control" TextMode="Time"
                                Text='<%# Eval("HoraSalida") %>' />
                            <%--Especialidades--%>
                            <asp:DropDownList ID="ddlEspecialidadesSabado" runat="server" CssClass="form-select mt-2">
                                <asp:ListItem Text="Especialidades" Value="" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%--PARA AGREGAR UN NUEVO HORARIO:--%>

                    <%--Horario desde--%>
                    <label class="form-label">Desde</label>
                    <asp:TextBox ID="txtNuevaHoraEntradaSabado" runat="server"
                        CssClass="form-control" TextMode="Time"
                        Text="" />

                    <%--Horario hasta--%>
                    <label class="form-label mt-2">Hasta</label>
                    <asp:TextBox ID="txtNuevaHoraSalidaSabado" runat="server"
                        CssClass="form-control" TextMode="Time"
                        Text="" />

                    <%--Especialidades--%>
                    <asp:DropDownList ID="ddlEspNuevoHorarioSabado" runat="server" CssClass="form-select mt-2">
                    </asp:DropDownList>
                    <%--Boton confirmar horario--%>
                    <div class="text-center mt-2">
                        <asp:Button ID="btnAñadirHorarioSabado" runat="server" Text="Añadir Horario"
                            CssClass="btn btn-primary"
                            CommandArgument="6"
                            OnCommand="btnAñadirHorario_Command" /><%--6 = Sabado--%>
                    </div>
                </div>
            </div>
        </div>

        <%--Domingo--%>
        <div class="col-md-3 mt-2">
            <div class="card shadow-sm mb-3">
                <h4 class="card-title ms-3 mt-2 mb-2">Domingo</h4>

                <%--Checkbox Dia no disponible--%>
                <div class="form-check ms-3 mb-2">
                    <asp:CheckBox ID="chkNoDisponibleDomingo" runat="server"
                        CssClass="form-check-input" AutoPostBack="true" />

                    <asp:Label ID="lblDomingo" runat="server" Text="Marcar como no disponible"
                        CssClass="form-check-label" />
                </div>
                <%-- Tarjeta para CADA bloque --%>
                <div class="card-body">
                    <%--REPEATER--%>
                    <asp:Repeater runat="server"
                        ID="repHorarioDomingo" OnItemDataBound="repHorarioDomingo_ItemDataBound">
                        <ItemTemplate>

                            <%-- Botón de borrar --%>
                            <div class="text-end mt-1 mb-1">
                                <asp:LinkButton ID="btnBorrarBloqueDomingo" CommandArgument='<%# Eval("Id")%>' OnCommand="btnBorrarBloque_Command"
                                    runat="server"
                                    CssClass="btn btn-danger btn-sm">
                                    <i class="bi bi-trash"></i>
                                </asp:LinkButton>

                            </div>

                            <%--Horario desde--%>
                            <label class="form-label">Desde</label>
                            <asp:TextBox ID="txtHoraEntradaDomingo" runat="server"
                                CssClass="form-control" TextMode="Time"
                                Text='<%# Eval("HoraEntrada") %>' />

                            <%--Horario hasta--%>
                            <label class="form-label mt-2">Hasta</label>
                            <asp:TextBox ID="txtHoraSalidaDomingo" runat="server"
                                CssClass="form-control" TextMode="Time"
                                Text='<%# Eval("HoraSalida") %>' />

                            <%--Especialidades--%>
                            <asp:DropDownList ID="ddlEspecialidadesDomingo" runat="server" CssClass="form-select mt-2">
                                <asp:ListItem Text="Especialidades" Value="" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%--PARA AGREGAR UN NUEVO HORARIO:--%>

                    <%--Horario desde--%>
                    <label class="form-label">Desde</label>
                    <asp:TextBox ID="txtNuevaHoraEntradaDomingo" runat="server"
                        CssClass="form-control" TextMode="Time"
                        Text="" />

                    <%--Horario hasta--%>
                    <label class="form-label mt-2">Hasta</label>
                    <asp:TextBox ID="txtNuevaHoraSalidaDomingo" runat="server"
                        CssClass="form-control" TextMode="Time"
                        Text="" />

                    <%--Especialidades--%>
                    <asp:DropDownList ID="ddlEspNuevoHorarioDomingo" runat="server" CssClass="form-select mt-2">
                    </asp:DropDownList>
                    <%--Boton confirmar horario--%>
                    <div class="text-center mt-2">
                        <asp:Button ID="btnAñadirHorarioDomingo" runat="server" Text="Añadir Horario"
                            CssClass="btn btn-primary"
                            CommandArgument="7"
                            OnCommand="btnAñadirHorario_Command" /><%--7 = Domingo--%>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
