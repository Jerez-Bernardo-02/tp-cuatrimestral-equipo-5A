<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MedicoHorarios.aspx.cs" Inherits="Presentacion.MedicoHorarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-md-8 mt-2 mb-2">
        <asp:DropDownList OnSelectedIndexChanged="ddlMedicos_SelectedIndexChanged" ID="ddlMedicos" 
            runat="server" CssClass="form-select mt-2" AutoPostBack="true">
        </asp:DropDownList>
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
                        ID="repHorarioLunes" OnItemDataBound="repHorarioLunes_ItemDataBound">
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
                            <asp:DropDownList ID="ddlNuevaEspecialidadesLunes" runat="server" CssClass="form-select mt-2">
                            </asp:DropDownList>
                        <%--Boton confirmar horario--%>
                    <div class="text-center mt-2">
                        <asp:Button ID="btnAñadirHorarioLunes" runat="server" Text="Añadir Horario"
                            CssClass="btn btn-primary"
                            OnClick="btnAñadirHorarioLunes_Click" />
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
                    <asp:CheckBox ID="CheckBox1" runat="server"
                        CssClass="form-check-input" AutoPostBack="true" />

                    <asp:Label ID="Label1" runat="server" Text="Marcar como no disponible"
                        CssClass="form-check-label" />
                </div>
                <%-- Tarjeta para CADA bloque --%>
                <div class="card-body">
                    <%-- Botón de borrar --%>
                    <div class="text-end mt-1 mb-1">
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Delete"
                            CssClass="btn btn-danger btn-sm">
                    <i class="bi bi-trash"></i>
                        </asp:LinkButton>

                    </div>

                    <%--Horario desde--%>
                    <label class="form-label">Desde</label>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" TextMode="Time" Text="" />

                    <%--Horario hasta--%>
                    <label class="form-label mt-2">Hasta</label>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" TextMode="Time" Text="" />

                    <%--Especialidades--%>
                    <asp:DropDownList ID="ddlEspecialidadesMartes" runat="server" CssClass="form-select mt-2">
                        <asp:ListItem Text="Especialidades" Value="" />
                    </asp:DropDownList>

                    <%--Boton confirmar horario--%>
                    <div class="text-center mt-2">
                        <asp:Button ID="Button1" runat="server" Text="Añadir Horario"
                            CssClass="btn btn-primary"
                            OnClick="btnAñadirHorarioLunes_Click" />
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
                    <asp:CheckBox ID="CheckBox2" runat="server"
                        CssClass="form-check-input" AutoPostBack="true" />

                    <asp:Label ID="Label2" runat="server" Text="Marcar como no disponible"
                        CssClass="form-check-label" />
                </div>
                <%-- Tarjeta para CADA bloque --%>
                <div class="card-body">
                    <%-- Botón de borrar --%>
                    <div class="text-end mt-1 mb-1">
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Delete"
                            CssClass="btn btn-danger btn-sm">
                    <i class="bi bi-trash"></i>
                        </asp:LinkButton>

                    </div>

                    <%--Horario desde--%>
                    <label class="form-label">Desde</label>
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" TextMode="Time" Text="" />

                    <%--Horario hasta--%>
                    <label class="form-label mt-2">Hasta</label>
                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" TextMode="Time" Text="" />

                    <%--Especialidades--%>
                    <asp:DropDownList ID="ddlEspecialidadesMiercoles" runat="server" CssClass="form-select mt-2">
                        <asp:ListItem Text="Especialidades" Value="" />
                    </asp:DropDownList>

                    <%--Boton confirmar horario--%>
                    <div class="text-center mt-2">
                        <asp:Button ID="Button2" runat="server" Text="Añadir Horario"
                            CssClass="btn btn-primary"
                            OnClick="btnAñadirHorarioLunes_Click" />
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
                    <asp:CheckBox ID="CheckBox3" runat="server"
                        CssClass="form-check-input" AutoPostBack="true" />

                    <asp:Label ID="Label3" runat="server" Text="Marcar como no disponible"
                        CssClass="form-check-label" />
                </div>
                <%-- Tarjeta para CADA bloque --%>
                <div class="card-body">
                    <%-- Botón de borrar --%>
                    <div class="text-end mt-1 mb-1">
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Delete"
                            CssClass="btn btn-danger btn-sm">
                    <i class="bi bi-trash"></i>
                        </asp:LinkButton>

                    </div>

                    <%--Horario desde--%>
                    <label class="form-label">Desde</label>
                    <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" TextMode="Time" Text="" />

                    <%--Horario hasta--%>
                    <label class="form-label mt-2">Hasta</label>
                    <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control" TextMode="Time" Text="" />

                    <%--Especialidades--%>
                    <asp:DropDownList ID="ddlEspecialidadesJueves" runat="server" CssClass="form-select mt-2">
                        <asp:ListItem Text="Especialidades" Value="" />
                    </asp:DropDownList>

                    <%--Boton confirmar horario--%>
                    <div class="text-center mt-2">
                        <asp:Button ID="Button3" runat="server" Text="Añadir Horario"
                            CssClass="btn btn-primary"
                            OnClick="btnAñadirHorarioLunes_Click" />
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
                    <asp:CheckBox ID="CheckBox4" runat="server"
                        CssClass="form-check-input" AutoPostBack="true" />

                    <asp:Label ID="Label4" runat="server" Text="Marcar como no disponible"
                        CssClass="form-check-label" />
                </div>
                <%-- Tarjeta para CADA bloque --%>
                <div class="card-body">
                    <%-- Botón de borrar --%>
                    <div class="text-end mt-1 mb-1">
                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Delete"
                            CssClass="btn btn-danger btn-sm">
                    <i class="bi bi-trash"></i>
                        </asp:LinkButton>

                    </div>

                    <%--Horario desde--%>
                    <label class="form-label">Desde</label>
                    <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control" TextMode="Time" Text="" />

                    <%--Horario hasta--%>
                    <label class="form-label mt-2">Hasta</label>
                    <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control" TextMode="Time" Text="" />

                    <%--Especialidades--%>
                    <asp:DropDownList ID="ddlEspecialidadesViernes" runat="server" CssClass="form-select mt-2">
                        <asp:ListItem Text="Especialidades" Value="" />
                    </asp:DropDownList>

                    <%--Boton confirmar horario--%>
                    <div class="text-center mt-2">
                        <asp:Button ID="Button4" runat="server" Text="Añadir Horario"
                            CssClass="btn btn-primary"
                            OnClick="btnAñadirHorarioLunes_Click" />
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
                    <asp:CheckBox ID="CheckBox5" runat="server"
                        CssClass="form-check-input" AutoPostBack="true" />

                    <asp:Label ID="Label5" runat="server" Text="Marcar como no disponible"
                        CssClass="form-check-label" />
                </div>
                <%-- Tarjeta para CADA bloque --%>
                <div class="card-body">
                    <%-- Botón de borrar --%>
                    <div class="text-end mt-1 mb-1">
                        <asp:LinkButton ID="LinkButton5" runat="server" CommandName="Delete"
                            CssClass="btn btn-danger btn-sm">
                    <i class="bi bi-trash"></i>
                        </asp:LinkButton>

                    </div>

                    <%--Horario desde--%>
                    <label class="form-label">Desde</label>
                    <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control" TextMode="Time" Text="" />

                    <%--Horario hasta--%>
                    <label class="form-label mt-2">Hasta</label>
                    <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control" TextMode="Time" Text="" />

                    <%--Especialidades--%>
                    <asp:DropDownList ID="ddlEspecialidadesSabado" runat="server" CssClass="form-select mt-2">
                        <asp:ListItem Text="Especialidades" Value="" />
                    </asp:DropDownList>

                    <%--Boton confirmar horario--%>
                    <div class="text-center mt-2">
                        <asp:Button ID="Button5" runat="server" Text="Añadir Horario"
                            CssClass="btn btn-primary"
                            OnClick="btnAñadirHorarioLunes_Click" />
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
                    <asp:CheckBox ID="CheckBox6" runat="server"
                        CssClass="form-check-input" AutoPostBack="true" />

                    <asp:Label ID="Label6" runat="server" Text="Marcar como no disponible"
                        CssClass="form-check-label" />
                </div>
                <%-- Tarjeta para CADA bloque --%>
                <div class="card-body">
                    <%-- Botón de borrar --%>
                    <div class="text-end mt-1 mb-1">
                        <asp:LinkButton ID="LinkButton6" runat="server" CommandName="Delete"
                            CssClass="btn btn-danger btn-sm">
                    <i class="bi bi-trash"></i>
                        </asp:LinkButton>

                    </div>

                    <%--Horario desde--%>
                    <label class="form-label">Desde</label>
                    <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control" TextMode="Time" Text="" />

                    <%--Horario hasta--%>
                    <label class="form-label mt-2">Hasta</label>
                    <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control" TextMode="Time" Text="" />

                    <%--Especialidades--%>
                    <asp:DropDownList ID="ddlEspecialidadesDomingo" runat="server" CssClass="form-select mt-2">
                        <asp:ListItem Text="Especialidades" Value="" />
                    </asp:DropDownList>

                    <%--Boton confirmar horario--%>
                    <div class="text-center mt-2">
                        <asp:Button ID="Button6" runat="server" Text="Añadir Horario"
                            CssClass="btn btn-primary"
                            OnClick="btnAñadirHorarioLunes_Click" />
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
