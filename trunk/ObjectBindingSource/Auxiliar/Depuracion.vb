Imports System.Reflection
Imports System.IO

'#  Released under the MIT license, reproduced below:
'#  ======================================================================
'#  Copyright (C)  2010  Daniel Prado Velasco' <dpradov@gmail.com>
'#
'#                         All Rights Reserved
'#
'# Permission is hereby granted, free of charge, to any person
'# obtaining a copy of this software and associated documentation
'# files (the "Software"), to deal in the Software without
'# restriction, including without limitation the rights to use,
'# copy, modify, merge, publish, distribute, sublicense, and/or sell
'# copies of the Software, and to permit persons to whom the
'# Software is furnished to do so, subject to the following
'# conditions:
'#
'# The above copyright notice and this permission notice shall be
'# included in all copies or substantial portions of the Software.
'#
'# THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
'# EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
'# OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
'# NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
'# HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
'# WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
'# FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
'# OTHER DEALINGS IN THE SOFTWARE.
'#
'# -----------------
'# URLs:
'#  http://code.google.com/p/object-binding-source/


''' <summary>
''' Contains helper functions related to debugging
''' </summary>
''' <remarks></remarks>
Public Module DBG

    Public MaxDebugLevel As Decimal = -1

    ' NOTA: Con la ayuda de la aplicación SearchAndReplace, que permite reemplazos masivos con el uso de expresiones regulares, podemos reemplazar 
    '       líneas de la forma
    '
    '   Console.WriteLine(...)
    '
    ' Por otras como:
    '
    '   DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format(...)))
    '
    ' Para ello, teniendo marcada la opción de 'Regular Expression' utilizamos los siguientes valores antes de pulsar en el botón 'Replace':
    ' Search For:   'Console.WriteLine\(*\)
    ' Replace With: DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format(%1)))
    '
    ' (Las líneas Console.WriteLine no comentadas las podemos reemplazar por algo equivalente y con nivel 0, por ejemplo)


    'Console.WriteLine\(*\)
    'DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format(%1)))

#If DEBUG Then
    Public NotifyOnMessagesIncludingERROR As Boolean = True

    Private _OutputLog As System.IO.TextWriter = Nothing

    Public Sub SetLogFile(ByVal file As String, Optional ByVal Append As Boolean = True)

        Try
            Dim sw = New StreamWriter(file, Append)
            sw.AutoFlush = True
            Console.SetOut(sw)
            _OutputLog = sw

        Catch e As IOException
            Dim errorWriter As TextWriter = Console.Error
            errorWriter.WriteLine(MessageError(e))
            SetStandardOutputLog()
        End Try

    End Sub

    Public Sub SetStandardOutputLog()
        If _OutputLog IsNot Nothing Then
            _OutputLog.Close()
            _OutputLog.Dispose()
            _OutputLog = Nothing
        End If

        Dim standardOutput As New StreamWriter(Console.OpenStandardOutput())
        standardOutput.AutoFlush = True
        Console.SetOut(standardOutput)
        _OutputLog = standardOutput
    End Sub

    Public Function Log(ByVal Nivel As Integer, ByVal Cadena As String, Optional ByVal NivelIndentacion As Integer = 0, Optional ByVal MostrarInstante As Boolean = True) As Boolean
        If Nivel > MaxDebugLevel Then Exit Function

        Dim cadenaLog As String
        If NivelIndentacion < 0 Then NivelIndentacion = 0
        Dim sep = New String(" "c, (NivelIndentacion + 1) * 2)

        If MostrarInstante Then
            cadenaLog = String.Format("[{0}]{1}-{2}{3}", Nivel, Now.ToLongTimeString, sep, Cadena)
        Else
            cadenaLog = String.Format("[{0}]{1}{2}", Nivel, sep, Cadena)
        End If
        Console.WriteLine(cadenaLog)

        If NotifyOnMessagesIncludingERROR AndAlso Cadena.Contains("ERROR") Then
            MsgBox(Cadena, MsgBoxStyle.Exclamation, "String 'ERROR' located")
        End If

    End Function

#Else

    Public Function Log(ByVal Nivel As Integer, ByVal Cadena As String, Optional ByVal NivelIndentacion As Integer = 0, Optional ByVal MostrarInstante As Boolean = True) As Boolean
        If Nivel > MaxDebugLevel Then Exit Function

        If NivelIndentacion < 0 Then NivelIndentacion = 0
        Dim sep = New String(" "c, (NivelIndentacion + 1) * 2)

        If MostrarInstante Then
            Cadena = String.Format("{0}-{1}{2}", Now.ToLongTimeString, sep, Cadena)
        Else
            Cadena = String.Format("{0}{1}", sep, Cadena)
        End If
        Console.WriteLine(Cadena)
    End Function

#End If

    Public Function ChkNivel(ByVal nivel As Integer) As Boolean
        If nivel <= MaxDebugLevel Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Procedimiento utilizado como ayuda para permitir construir sentencias de depuración que sólo se evalúen cuando realmente el nivel de depuración
    ''' los contemplará. Por optimización y para evitar posibles excepciones provocadas al intentar construir la cadena
    ''' </summary>
    ''' <param name="foo"></param>
    ''' <remarks>
    ''' Ejemplo de uso:
    ''' DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("ERROR: {0}", ex.Message)))
    ''' </remarks>
    Public Sub Foo(ByVal foo As Boolean)
    End Sub

    Public Function MessageError(ByVal ex As Exception) As String
        If TypeOf (ex) Is TargetInvocationException Then
            Return ex.InnerException.Message
        Else
            Return ex.Message
        End If
    End Function

End Module
