Public Module [Global]
    ''' <summary>
    ''' Controla si los mensajes de depuración deben o no mostrarse, como condición adicional además del nivel de éstos. Por defecto se muestran
    ''' </summary>
    Public ShowDBG As Boolean = True

    Public Function DBG_ChkNivel(ByVal nivel As Integer) As Boolean
        Return ShowDBG AndAlso DBG.ChkNivel(nivel)
    End Function

    Public Sub DBG_SaltoLinea(ByVal nivel As Integer)
        DBG.Foo(DBG_ChkNivel(nivel) AndAlso DBG.Log(nivel, ""))
    End Sub

End Module
