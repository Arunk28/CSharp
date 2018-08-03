using System;
using System.Collections.Generic;


namespace azureclass
{
    public class Body
    {
        public string contentType { get; set; }
        public string content { get; set; }
    }

    public class EmailAddress
    {
        public string name { get; set; }
        public string address { get; set; }
    }

    public class Sender
    {
        public EmailAddress emailAddress { get; set; }
    }

    public class EmailAddress2
    {
        public string name { get; set; }
        public string address { get; set; }
    }

    public class From
    {
        public EmailAddress2 emailAddress { get; set; }
    }

    public class EmailAddress3
    {
        public string name { get; set; }
        public string address { get; set; }
    }

    public class ToRecipient
    {
        public EmailAddress3 emailAddress { get; set; }
    }

    public class EmailAddress4
    {
        public string name { get; set; }
        public string address { get; set; }
    }

    public class EmailAddress5
    {
        public string name { get; set; }
        public string address { get; set; }
    }

    public class EmailAddress6
    {
        public string name { get; set; }
        public string address { get; set; }
    }

    public class ReplyTo
    {
        public EmailAddress4 emailAddress { get; set; }
    }

    public class cc
    {
        public EmailAddress5 emailAddress { get; set; }
    }
    public class bcc
    {
        public EmailAddress6 emailAddress { get; set; }
    }

    public class Mail
    {
        public string id { get; set; }
        public DateTime createdDateTime { get; set; }
        public DateTime lastModifiedDateTime { get; set; }
        public string changeKey { get; set; }
        public List<object> categories { get; set; }
        public DateTime receivedDateTime { get; set; }
        public DateTime sentDateTime { get; set; }
        public bool hasAttachments { get; set; }
        public string internetMessageId { get; set; }
        public string subject { get; set; }
        public string bodyPreview { get; set; }
        public string importance { get; set; }
        public string parentFolderId { get; set; }
        public string conversationId { get; set; }
        public object isDeliveryReceiptRequested { get; set; }
        public bool isReadReceiptRequested { get; set; }
        public bool isRead { get; set; }
        public bool isDraft { get; set; }
        public string webLink { get; set; }
        public string inferenceClassification { get; set; }
        public Body body { get; set; }
        public Sender sender { get; set; }
        public From from { get; set; }
        public List<ToRecipient> toRecipients { get; set; }
        public List<cc> ccRecipients { get; set; }
        public List<bcc> bccRecipients { get; set; }
        public List<ReplyTo> replyTo { get; set; }
    }
}