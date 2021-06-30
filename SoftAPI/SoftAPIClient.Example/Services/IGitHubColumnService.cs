﻿using SoftAPIClient.Attributes;
using System;
using System.Collections.Generic;
using RestSharp;
using SoftAPIClient.Example.Core;
using SoftAPIClient.Example.Interceptors;
using SoftAPIClient.Example.Models;
using SoftAPIClient.MetaData;

namespace SoftAPIClient.Example.Services
{
    [Client(DynamicUrlKey = "GitHub:Api", Path = "/projects", DynamicUrlType = typeof(DynamicConfiguration), RequestInterceptor = typeof(GitHubAuthenticationRequestInterceptor))]
    public interface IGitHubColumnService
    {
        [Log("Send GET request to 'GitHub API' for getting column by Id={0}")]
        [RequestMapping(Method.GET, Path = "/columns/{column_id}")]
        Func<ResponseGeneric2<GitHubResponse, GitHubErrorResponse>> GetColumnById([PathParameter("column_id")] int columnId);

        [Log("Send PATCH request to 'GitHub API' for updating column by Id={0} with the next data={1}")]
        [RequestMapping(Method.PATCH, Path = "/columns/{column_id}")]
        Func<ResponseGeneric2<GitHubResponse, GitHubErrorResponse>> UpdateColumnById([PathParameter("column_id")] int columnId, 
            [Body] GitHubBodyRequest body);

        [Log("Send DELETE request to 'GitHub API' for removing column by Id={0}")]
        [RequestMapping(Method.DELETE, Path = "/{projectId}")]
        Func<ResponseGeneric<GitHubErrorResponse>> DeleteColumnById([PathParameter("projectId")] int projectId);

        [Log("Send POST request to 'GitHub API' for moving column by Id={0} with the next data={1}")]
        [RequestMapping(Method.POST, Path = "/columns/{column_id}/moves")]
        Func<ResponseGeneric2<GitHubResponse, GitHubErrorResponse>> MoveColumnById([PathParameter("column_id")] int columnId, 
            [Body] GitHubBodyRequest body);

        [Log("Send GET request to 'GitHub API' for getting columns by project Id={0} with the next pagination: page={1}, count={2}")]
        [RequestMapping(Method.GET, Path = "/projects/{project_id}/columns")]
        Func<ResponseGeneric<List<GitHubResponse>>> GetListProjectColumns([PathParameter("project_id")] int projectId, 
            [QueryParameter("page")] int? page = 1, [QueryParameter("per_page")] int? perPage = 30);

        [Log("Send POST request to 'GitHub API' for creating column by project Id={0} with the body={1}")]
        [RequestMapping(Method.POST, Path = "/projects/{project_id}/columns")]
        Func<ResponseGeneric<List<GitHubResponse>>> CreateProjectColumn([PathParameter("project_id")] int projectId,
            [Body] GitHubBodyRequest body);
    }
}
