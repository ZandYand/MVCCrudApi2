Imports System.Data
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Http.Description
Imports MVCCrudApi2

Namespace Controllers
    Public Class peopleController
        Inherits System.Web.Http.ApiController

        Private db As New CRUDejemploEntities1

        ' GET: api/people
        Function Getpeople() As IQueryable(Of people)
            Return db.people
        End Function

        ' GET: api/people/5
        <ResponseType(GetType(people))>
        Function Getpeople(ByVal id As Integer) As IHttpActionResult
            Dim people As people = db.people.Find(id)
            If IsNothing(people) Then
                Return NotFound()
            End If

            Return Ok(people)
        End Function

        ' PUT: api/people/5
        <ResponseType(GetType(Void))>
        Function Putpeople(ByVal id As Integer, ByVal people As people) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            If Not id = people.id Then
                Return BadRequest()
            End If

            db.Entry(people).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As DbUpdateConcurrencyException
                If Not (peopleExists(id)) Then
                    Return NotFound()
                Else
                    Throw
                End If
            End Try

            Return StatusCode(HttpStatusCode.NoContent)
        End Function

        ' POST: api/people
        <ResponseType(GetType(people))>
        Function Postpeople(ByVal people As people) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            db.people.Add(people)
            db.SaveChanges()

            Return CreatedAtRoute("DefaultApi", New With {.id = people.id}, people)
        End Function

        ' DELETE: api/people/5
        <ResponseType(GetType(people))>
        Function Deletepeople(ByVal id As Integer) As IHttpActionResult
            Dim people As people = db.people.Find(id)
            If IsNothing(people) Then
                Return NotFound()
            End If

            db.people.Remove(people)
            db.SaveChanges()

            Return Ok(people)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Function peopleExists(ByVal id As Integer) As Boolean
            Return db.people.Count(Function(e) e.id = id) > 0
        End Function
    End Class
End Namespace