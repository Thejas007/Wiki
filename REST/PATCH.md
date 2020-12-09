
# PATCH
https://developers.google.com/tasks/performance#patch

You can also avoid sending unnecessary data when modifying resources. To send updated data only for the specific fields that you’re changing, use the HTTP PATCH verb. The patch semantics described in this document are different (and simpler) than they were for the older, GData implementation of partial update.

The short example below shows how using patch minimizes the data you need to send to make a small update.

Example
This example shows a simple patch request to update only the title of a generic (fictional) "Demo" API resource. The resource also has a comment, a set of characteristics, status, and many other fields, but this request only sends the title field, since that's the only field being modified:


PATCH https://www.googleapis.com/demo/v1/324
Authorization: Bearer your_auth_token
Content-Type: application/json

{
  "title": "New title"
}

Semantics of a patch request
The body of the patch request includes only the resource fields you want to modify. When you specify a field, you must include any enclosing parent objects, just as the enclosing parents are returned with a partial response. The modified data you send is merged into the data for the parent object, if there is one.

Add: To add a field that doesn't already exist, specify the new field and its value.
Modify: To change the value of an existing field, specify the field and set it to the new value.
Delete: To delete a field, specify the field and set it to null. For example, "comment": null. You can also delete an entire object (if it is mutable) by setting it to null. If you are using the Java API Client Library, use Data.NULL_STRING instead; for details, see JSON null.
Note about arrays: Patch requests that contain arrays replace the existing array with the one you provide. You cannot modify, add, or delete items in an array in a piecemeal fashion.

https://googleapis.github.io/google-http-java-client/json.html#json_null
