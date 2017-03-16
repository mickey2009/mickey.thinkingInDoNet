using Mickey.Core.ComponentModel;
using Microsoft.AspNet.Identity;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mickey.EntityFramework.Identities
{
    public class PasswordValidator : IIdentityValidator<string>
    {
        string m_PasswordRegexPattern;

        public PasswordValidator(string pattern)
        {
            Requires.NotNull(pattern, "pattern");
            m_PasswordRegexPattern = pattern;
        }

        public Task<IdentityResult> ValidateAsync(string item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            var re = new Regex(m_PasswordRegexPattern);
            if (re.IsMatch(item))
            {
                return Task.FromResult(IdentityResult.Success);
            }
            return Task.FromResult(IdentityResult.Failed("7~20个字符，字母+数字组合"));
        }
    }
}
