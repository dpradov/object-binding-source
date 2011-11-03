Imports System.Reflection

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

    Public Property MaxNivelDepuracion() As Decimal
        Get
            Return _MaxNivelDepuracion
        End Get
        Set(ByVal value As Decimal)
            _MaxNivelDepuracion = value
        End Set
    End Property
    Public _MaxNivelDepuracion As Decimal = -1

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

    Public Function Log(ByVal Nivel As Integer, ByVal Cadena As String, Optional ByVal NivelIndentacion As Integer = 0, Optional ByVal MostrarInstante As Boolean = True) As Boolean
        If Nivel > MaxNivelDepuracion Then Exit Function

        Dim cadenaLog As String
        If NivelIndentacion < 0 Then NivelIndentacion = 0
        Dim sep = New String(" "c, (NivelIndentacion + 1) * 2)

        If MostrarInstante Then
            cadenaLog = String.Format("[{0}]{1}-{2}{3}", Nivel, Now.ToLongTimeString, sep, Cadena)
        Else
            cadenaLog = String.Format("[{0}]{1}{2}", Nivel, sep, Cadena)
        End If
        Console.WriteLine(cadenaLog)
    End Function

#Else

    Public Function Log(ByVal Nivel As Integer, ByVal Cadena As String, Optional ByVal NivelIndentacion As Integer = 0, Optional ByVal MostrarInstante As Boolean = True) As Boolean
        If Nivel > MaxNivelDepuracion Then Exit Function

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
        If nivel <= MaxNivelDepuracion Then
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

    Public Sub MostrarPilaLlamadas(Optional ByVal numFramesAIgnorar As Integer = 0, Optional ByVal numMaximoFramesAMostrar As Integer = Integer.MaxValue)
        Dim st As New StackTrace(True)
        Dim k As Integer = 0

        For i As Integer = 1 + numFramesAIgnorar To st.FrameCount - 1

            ' Note that high up the call stack, there is only one stack frame.
            Dim sf As StackFrame = st.GetFrame(i)
            Dim m As MethodBase = sf.GetMethod()
            If m.DeclaringType.FullName.StartsWith("System.") Then
                Exit For
            End If
            Console.WriteLine("  - {0}:{1}  ({2} linea: {3})", m.DeclaringType.FullName, m.Name, sf.GetFileName, sf.GetFileLineNumber)
            k += 1
            If k > numMaximoFramesAMostrar Then Exit For
        Next i
    End Sub

    Public Function MetodoLlamadaAnteriorA(ByVal llamada As String) As MethodBase
        Dim st As New StackTrace(True)
        Dim k As Integer = 0
        Dim m As MethodBase
        Dim sf As StackFrame

        For i As Integer = 1 To st.FrameCount - 1
            sf = st.GetFrame(i)
            m = sf.GetMethod()
            If m.Name = llamada Then
                Return st.GetFrame(i + 1).GetMethod
            End If
            If m.DeclaringType.FullName.StartsWith("System.") Then
                Exit For
            End If
        Next i

        Return Nothing
    End Function

    Public Sub MostrarMetodoActual()
        Dim st As New StackTrace(True)
        Dim m1 As MethodBase = st.GetFrame(1).GetMethod()
        If st.FrameCount < 3 Then
            Console.WriteLine("  - {0}:{1}", m1.DeclaringType.FullName, m1.Name)
        Else
            Dim m2 As MethodBase = st.GetFrame(2).GetMethod()
            Console.WriteLine("  - {0}:{1}  <<  {2}:{3}  ({4} linea: {5})", m1.DeclaringType.FullName, m1.Name, m2.DeclaringType.FullName, m2.Name, st.GetFrame(2).GetFileName, st.GetFrame(2).GetFileLineNumber)
        End If
    End Sub

    Public Function MetodoLlamador() As MethodBase
        Return New StackTrace(False).GetFrame(2).GetMethod()
    End Function

    Public Function MetodoPadre(Optional ByVal numSaltos As Integer = 1) As MethodBase
        Return New StackTrace(False).GetFrame(numSaltos + 1).GetMethod()
    End Function


    Public Function MensajeError(ByVal ex As Exception) As String
        If TypeOf (ex) Is TargetInvocationException Then
            Return ex.InnerException.Message
        Else
            Return ex.Message
        End If
    End Function

End Module
