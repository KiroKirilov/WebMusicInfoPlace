function buildNewComment(title, body, author, date, time) {
    return `
<div class="commentParent">
    <div class="commentHeader">
        <h6><b>${title}</b>, by ${author}, submitted on: ${date}, ${time}</h6>
        <a class="btn btn-primary newCommentButton">New</a>
    </div>
    <div class="commentBody">
        <p>${body}</p>
    </div>
</div>`
}