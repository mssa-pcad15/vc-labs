/**
* This stored procedure adds a timestamp to the incoming document and inserts it into the collection.
*
* @param {Object} document - The document to be inserted.
*/
function addTimestamp(document)
{
    var collection = getContext().getCollection();
    var collectionLink = collection.getSelfLink();

    // Add a timestamp to the document
    document.timestamp = new Date().toISOString();

    // Create the document
    var isAccepted = collection.createDocument(collectionLink, document, function(err, createdDocument) {
        if (err) throw new Error("Error creating document: " + err.message);
        getContext().getResponse().setBody(createdDocument);
    });

    if (!isAccepted) throw new Error("The document creation request was not accepted by the server.");
}
